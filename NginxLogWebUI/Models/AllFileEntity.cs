using System.Security.Cryptography.X509Certificates;

namespace NginxLogWebUI.Models
{
    public class AllFileEntity
    {
        public AllFileEntity()
        {
            nginxLogWebUISubfileFiles = new List<NginxLogWebUISubfileFile>();
            nginxLogWebUIDocuments = new List<NginxLogWebUIDocument>();
        }
        public List<NginxLogWebUISubfileFile> nginxLogWebUISubfileFiles { get; set; }
        public List<NginxLogWebUIDocument> nginxLogWebUIDocuments { get; set; }
    }
    /// <summary>
    /// 文件夹
    /// </summary>
    public class NginxLogWebUISubfileFile
    {
        public string AllRoot { get; set; }
        public string Type { get; set; } = "wjj";
        public string Root { get; set; }
        public string NginxLogWebUISubfileFileFileName { get; set; }
    }
    /// <summary>
    /// 文件
    /// </summary>
    public class NginxLogWebUIDocument
    {
        public string AllRoot { get; set; }
        public string Type { get; set; } = "wj";
        public string Root { get; set; }
        public string NginxLogWebUIDocumentFileName { get; set; }
    }
}
