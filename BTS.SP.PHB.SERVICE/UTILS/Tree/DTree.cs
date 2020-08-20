using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.UTILS.Tree
{
    public delegate void TreeVisitor<T>(T nodeData);
    public class DTree<T>
    {
        public T node;
        public LinkedList<DTree<T>> children;

        public DTree(T node)
        {
            this.node = node;
            children = new LinkedList<DTree<T>>();
        }

        public void AddChild(T node)
        {
            children.AddLast(new DTree<T>(node));
        }

        public void AddNode(DTree<T> node)
        {
            children.AddLast(node);
        }

        public DTree<T> GetChild(int i)
        {
            foreach (DTree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }
        public void Traverse(DTree<T> temp, TreeVisitor<T> visitor)
        {
            visitor(temp.node);
            foreach (DTree<T> kid in temp.children)
                Traverse(kid, visitor);
        }
        /*Làm phẳng Tree */
        public List<T> Flatten()
        {
            var temp = new[] { node }.Union(children.SelectMany(x => x.Flatten()));
            return temp.ToList<T>();
        }
    }
}
