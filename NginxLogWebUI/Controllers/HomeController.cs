using Microsoft.AspNetCore.Mvc;
using NginxLogWebUI.Fun.File;
using NginxLogWebUI.Models;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace NginxLogWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string Os = string.Empty;
        private readonly string RootPath = string.Empty;
        private readonly string NowPath = FileHelper.GetCurrentDirectory();
        private readonly string HTMLPath = FileHelper.GetCurrentDirectory() + "\\wwwroot\\goaccess\\";
        public HomeController(ILogger<HomeController> logger)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows 相关逻辑
                Os = "Windows";
                RootPath = "C:/";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Linux 相关逻辑
                Os = "Linux";
                RootPath = "/";
                HTMLPath = HTMLPath.Replace("\\", "/");
            }
            if (!Directory.Exists(HTMLPath))
            {
                Directory.CreateDirectory(HTMLPath);
            }
            _logger = logger;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Index()
        {
            ViewBag.OS = Os;
            ViewBag.NowPath = NowPath;
            return View();
        }
        public IActionResult Use()
        {
            return View();
        }
        public IActionResult HtmlList(string f)
        {
            ViewBag.NowPath = HTMLPath + f;
            return View();
        }
        public IActionResult CreateLog(string f)
        {
            string NowPath = HTMLPath + f;
            ViewData["NowPath"] = NowPath;
            return View();
        }
        public IActionResult GetAllFileEntity(string f)
        {
            AllFileEntity allFileEntity = new AllFileEntity();
            if (string.IsNullOrEmpty(f)) { f = RootPath; }
            var l1 = FileHelper.GetFileList(f);
            var nginxLogWebUIDocuments = new List<NginxLogWebUIDocument>();
            foreach (var item in l1)
            {
                var info = FileHelper.GetInfo(item);
                nginxLogWebUIDocuments.Add(new NginxLogWebUIDocument()
                {
                    AllRoot = item,
                    Root = info.DirectoryName,
                    NginxLogWebUIDocumentFileName = info.Name
                });
            }
            var l2 = FileHelper.GetFilePath2(f);
            var nginxLogWebUISubfileFiles = new List<NginxLogWebUISubfileFile>();

            foreach (var item in l2)
            {
                var info = FileHelper.GetInfo(item);
                nginxLogWebUISubfileFiles.Add(new NginxLogWebUISubfileFile()
                {
                    AllRoot = item,
                    Root = info.DirectoryName,
                    NginxLogWebUISubfileFileFileName = info.Name
                });
            }
            allFileEntity.nginxLogWebUISubfileFiles = nginxLogWebUISubfileFiles;
            allFileEntity.nginxLogWebUIDocuments = nginxLogWebUIDocuments;
            return Json(new AllFileEntityToLayuiTrees(allFileEntity));
        }
        public string CLog(string f, string o)
        {
            // goaccess -f /www/wwwlogs/access.log --log-format=COMBINED -a >/root/nginxhtmllog/go-access.html
            string shell = "goaccess -f " + o + " --log-format=COMBINED -a >" + f + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            try
            {
                string outstr = string.Empty;
                if (Os == "Linux")
                {
                    shell = shell.Replace("\\", "/");
                    outstr = ShellUtil.Bash(shell);
                }
                else
                {
                    outstr = ShellUtil.Cmd(shell, null);
                }
                return outstr;
            }
            catch (Exception E)
            {
                return E.Message;
            }
            
        }
        public IActionResult GetHtmlList(string f)
        {
            var FDIR = HTMLPath + f;

            List<string> files = FileHelper.GetFileList(FDIR).ToList();
            List<FileEntity> r = new List<FileEntity>();
            foreach (var item in files)
            {
                var info = FileHelper.GetInfo(item);
                r.Add(new FileEntity()
                {
                    f = info.Name,
                    //s = Math.Ceiling(info.Length / 1024.0) + " KB",
                    s = info.Length.ToString() + " B",
                    d = info.CreationTimeUtc.ToString(),
                    dic = info.FullName
                });
            }
            LayuiTableEntity layuiTableEntity = new LayuiTableEntity()
            {
                count = r.Count,
                data = r
            };
            return Json(layuiTableEntity);
        }
        public HttpResponseMessage RemoveAll()
        {
            try
            {
                List<string> files = FileHelper.GetFilePath(HTMLPath).ToList();

                foreach (var item in files)
                {
                    var RDIR = NowPath + "\\wwwroot\\goaccess\\" + item;
                    if (Os == "Linux")
                    {
                        RDIR = RDIR.Replace("\\", "/");
                    }
                    Directory.Delete(RDIR, true);
                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("删除成功"),
                    ReasonPhrase = "Success"
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("删除失败"),
                    ReasonPhrase = "Fail"
                };
                throw;
            }

        }
        public HttpResponseMessage CreatePath(string dir)
        {
            var CDIR = NowPath + "\\wwwroot\\goaccess\\" + dir;
            if (Os == "Linux")
            {
                CDIR = CDIR.Replace("\\", "/");
            }
            if (!Directory.Exists(CDIR))
            {
                Directory.CreateDirectory(CDIR);
                try
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("创建成功"),
                        ReasonPhrase = "Success"
                    };
                }
                catch (Exception E)
                {

                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("创建失败"),
                        ReasonPhrase = E.Message
                    };
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent("该文件已存在"),
                    ReasonPhrase = "Already Exists"
                };
            }
        }
        public IActionResult GetRootFileList()
        {
            List<string> files = FileHelper.GetFilePath(HTMLPath).ToList();
            List<string> r = new List<string>();
            foreach (var item in files)
            {
                var name = item.Replace(HTMLPath, "");
                r.Add(name);
            }
            return Json(r);
        }
        public List<string> GetFileList(string path)
        {
            return FileHelper.GetFileList(HTMLPath + path);
        }
        public List<string> GetFileList2(string path)
        {
            return FileHelper.GetFileList(path);
        }
    }
}