using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Tree 
    {
        public Tree()
        {
            Children = new List<Tree>();
        }
        public int id { get; set; }
        public string title { get; set; }
        public string route { get; set; }
        public int son { get; set; }
        public int parent { get; set; }
        public List<PermissionStatus> cards { get; set; }
        public List<PermissionStatus> permissions { get; set; }
        public List<Tree> Children { get; set; }
    }
}