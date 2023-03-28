using System.Security;

namespace NginxLogWebUI.Models
{
    public class FileEntity
    {
        /// <summary>
        /// 文件
        /// </summary>
        public string f { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public string d { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string s { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        public string dic { get; set; }
    }
}
