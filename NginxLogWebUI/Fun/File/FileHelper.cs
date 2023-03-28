using System.Diagnostics;

namespace NginxLogWebUI.Fun.File
{
    public class FileHelper
    {
        /// <summary>
        /// 获取目录下的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFilePath(string path)
        {
            if (Directory.Exists(path))
            {
                //文件路径
                string[] dir = Directory.GetDirectories(path);
                //文件名
                string[] names = new string[dir.Length];
                for (int i = 0; i < dir.Length; i++)
                {
                    //赋值文件命名
                    names[i] = Path.GetFileName(dir[i]);
                }
                return names;
            }
            else
            {
                return null;
            }
        }
        public static string[] GetFilePath2(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.GetDirectories(path); ;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前路径
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory() 
        {
            return System.IO.Directory.GetCurrentDirectory();
        }
        /// <summary>
        /// 递归获取目录下的所有文件清单
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetFileList(string path)
        {
            List<string> fileList = new List<string>();

            if (Directory.Exists(path) == true)
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    fileList.Add(file);
                }
            }
            return fileList;
        }

        public static FileInfo GetInfo(string path) 
        {
            return new FileInfo(path);
        }
    }
}
