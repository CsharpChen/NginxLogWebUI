
namespace NginxLogWebUI.Models
{
    public class AllFileEntityToLayuiTrees
    {
        public AllFileEntityToLayuiTrees() { }
        public AllFileEntityToLayuiTrees(AllFileEntity allFileEntity)
        {
            List<AllFileEntityToLayuiTree> tempallFileEntityToLayuiTrees = new List<AllFileEntityToLayuiTree>();
            foreach (var item in allFileEntity.nginxLogWebUIDocuments)
            {
                tempallFileEntityToLayuiTrees.Add(new AllFileEntityToLayuiTree()
                {
                    title = item.NginxLogWebUIDocumentFileName,
                    type = item.Type,
                    allname = item.AllRoot
                });
            }
            foreach (var item in allFileEntity.nginxLogWebUISubfileFiles)
            {
                var temp = new AllFileEntityToLayuiTree();
                temp.title = item.NginxLogWebUISubfileFileFileName;
                temp.type = item.Type;
                temp.allname = item.AllRoot;
                //temp.children.Add(new AllFileEntityToLayuiTree() { title = item.NginxLogWebUISubfileFileFileName, nginxLogWebUISubfileFile = item, type = item.Type });
                temp.children.Add(new AllFileEntityToLayuiTree() { });
                tempallFileEntityToLayuiTrees.Add(temp);
            }
            allFileEntityToLayuiTrees = tempallFileEntityToLayuiTrees;
        }
        public List<AllFileEntityToLayuiTree> allFileEntityToLayuiTrees { get; set; } = new List<AllFileEntityToLayuiTree>();
    }
    public class AllFileEntityToLayuiTree
    {
        public List<AllFileEntityToLayuiTree> children { get; set; } = new List<AllFileEntityToLayuiTree>();

        public string id { get; set; } = Guid.NewGuid().ToString();
        public string allname { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string titlepath { get; set; }
    }

}
