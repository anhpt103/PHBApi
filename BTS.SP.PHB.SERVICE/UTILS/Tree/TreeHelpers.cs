using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.UTILS.Tree
{
    public static class TreeHelpers
    {
        public static DTree<T> GenerateTree<T>(string propNameId, string propNameParentId, List<T> TList) where T : class
        {
            DTree<T> Tree = new DTree<T>(null);

            foreach (var item in TList)
            {
                if (item.GetType().GetProperty(propNameParentId).GetValue(item, null) == null)
                {
                    var temptree = create(propNameId, propNameParentId, new DTree<T>(item), TList);

                    Tree.AddNode(temptree);
                }

            };

            return Tree;
        }
        public static DTree<T> create<T>(string propNameId, string propNameParentId, DTree<T> TTree, List<T> TList) where T : class
        {
            string parentId = TTree.node.GetType().GetProperty(propNameId).GetValue(TTree.node, null).ToString();

            foreach (var item in TList)
            {
                string d = "";
                if (item.GetType().GetProperty(propNameParentId).GetValue(item, null) != null)
                {
                    d = item.GetType().GetProperty(propNameParentId).GetValue(item, null).ToString();
                }

                if (d == parentId && !string.IsNullOrEmpty(d) && !string.IsNullOrEmpty(parentId))
                {
                    var temptree = create(propNameId, propNameParentId, new DTree<T>(item), TList);

                    TTree.AddNode(temptree);
                }
            }

            return TTree;

        }
    }
}
