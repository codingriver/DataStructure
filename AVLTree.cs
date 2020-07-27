using System;
using System.Collections.Generic;

namespace Codingriver_AVL
{
    #region Node
    public class Node
    {
        public int Key;
        public Node Parent;//parent
        public Node L; //left
        public Node R; //right
        public int H; //height;

        public Node(int key, Node parent=null, Node left=null, Node right=null)
        {
            Key = key;
            Parent = parent;
            L = left;
            R = right;
        }


        #region 比较运算符
        public static bool operator >(Node a, Node b)
        {
            if (a is null || b is null)
            {
                return false;
            }
            return a.Key > b.Key;
        }
        public static bool operator >(Node a,int key)
        {
            if (a is null)
                return false;
            return a.Key > key;
        }
        public static bool operator >(int key,Node a)
        {
            if (a is null)
                return false;
            return key>a.Key  ;
        }

        public static bool operator <(Node a, int key)
        {
            if (a is null)
                return false;
            return a.Key < key;
        }
        public static bool operator <(int key, Node a)
        {
            if (a is null)
                return false;
            return key < a.Key;
        }
        public static bool operator >=(Node a, Node b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            return a.Key >= b.Key;
        }
        public static bool operator <(Node a, Node b)
        {
            if (a is null || b is null)
            {
                return false;
            }
            return a.Key < b.Key;
        }
        public static bool operator <=(Node a, Node b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            return a.Key <= b.Key;
        }
        public static bool operator ==(Node a, Node b)
        {
            if(a is null&& b is null)
            {
                return true;
            }
            else if(a is null || b is null)
            {
                return false;
            }

            return a.Key == b.Key;
        }
        public static bool operator !=(Node a, Node b)
        {
            if (a is null && b is null)
            {
                return false;
            }
            else if (a is null || b is null)
            {
                return true;
            }
            return a.Key != b.Key;
        }

        #endregion
    }
    #endregion



    /// <summary>
    /// AVL树
    /// AVL树是根据它的发明者G.M.Adelson-Velsky和E.M.Landis命名的。
    /// 它是最先发明的自平衡二叉查找树，也被称为高度平衡树。相比于"二叉查找树"，它的特点：AVL树中任何节点的两个子树的高度最大差别为1
    /// </summary>
    public class AVLTree
    {
        //树的根节点
        public Node Root;

        #region create node
        public Node CreateNode(int key, Node parent = null, Node left = null, Node right = null)
        {
            return new Node(key, parent, left, right);
        }
        public Node CreateLNode(int key, Node parent)
        {
            var n = CreateNode(key, parent);
            parent.L = n;
            return n;
        }
        public Node CreateRNode(int key, Node parent)
        {
            var n = CreateNode(key, parent);
            parent.R = n;
            return n;
        }
        #endregion


        #region base method
        public bool IsLeft(Node n)
        {
            return n.Parent != null && n.Parent.L == n;
        }
        public bool IsRight(Node n)
        {
            return n.Parent != null && n.Parent.R == n;
        }
        public bool IsRoot(Node n)
        {
            return n == Root;
        }

        public bool HasParent(Node n)
        {
            return n.Parent != null;
        }
        public bool HasChild(Node n)
        {
            return n.L != null || n.R != null;
        }
        public bool HasOneChild(Node n)
        {
            return n != null && (n.L != null && n.R == null || n.R != null && n.L == null);
        }
        public bool HasTwoChild(Node n)
        {
            return n != null && n.L != null && n.R != null;
        }
        public bool HasLeft(Node n)
        {
            return n != null && n.L != null;
        }
        public bool HasRight(Node n)
        {
            return n != null && n.R != null;
        }

        /// <summary>
        /// 树的深度
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int GetDepth(Node n)
        {
            return n == null ? 0 : GetDepth(n.Parent) + 1;
        }

        /// <summary>
        /// 二叉树第i层上的结点数目最大值
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int GetMaxNodeCountByDepth(int i)
        {
            if (i == 1||i==0)
                return i;
            return GetMaxNodeCountByDepth(i-1) * 2;
        }



        /// <summary>
        /// 树的高度
        /// 树的高度为最大层次。即空的二叉树的高度是0，非空树的高度等于它的最大层次(根的层次为1，根的子节点为第2层，依次类推)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int Height(Node a)
        {
            return a == null ? 0 : a.H;
        }

        public int MaxHeight(Node a, Node b)
        {
            return a == null ? (b == null ? 0 : b.H) : (b == null ? a.H : a.H > b.H ? a.H : b.H);
        }

        /// <summary>
        /// n子树中最大的节点
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Node Maximum(Node n)
        {
            if (n == null)
                return null;
            while (n.R != null)
                n = n.R;
            return n;
        }
        /// <summary>
        /// n子树中最小的节点
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Node Minimum(Node n)
        {
            if (n == null)
                return null;
            while (n.L != null)
                n = n.L;
            return n;
        }

        #endregion

        #region 前驱节点、后继节点
        /// <summary>
        /// 前驱节点
        /// 对一棵二叉树进行中序遍历，按照遍历后的顺序，当前节点的前一个节点为该节点的前驱节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node Predecessor(Node node)
        {
            Node n = null;
            if (node == null)
                return null;
            //1.如果有左子树，那么前驱节点是左子树中最大值
            else if (HasLeft(node))
                return Maximum(node.L);

            //2.1如果是右孩子，node的前驱节点为它的父节点
            else if (IsRight(node))
                return node.Parent;

            //2.2如果是左孩子则找到最低的父节点且父节点的右孩子是node所在的子树
            //另一种说法：如果是左孩子则找到node所在子树被称为右子树的父节点
            else if (IsLeft(node))
            {
                n = node.Parent;
                while (n != null)
                {
                    if (IsLeft(n))
                        n = n.Parent;
                    else
                        return n.Parent;
                }
            }
            return null;
        }

        /// <summary>
        /// 后继节点
        /// 对一棵二叉树进行中序遍历，按照遍历后的顺序，当前节点的后一个节点为该节点的后继节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node Successor(Node node)
        {
            Node n = null;

            //1.如果有右子树，那么后继节点是左子树中最大值
            if (HasRight(node))
                return Minimum(node.R);
            //2.1 如果是左孩子，node的后继节点为它的父节点
            else if (IsLeft(node))
                return node.Parent;

            //2.2 如果是右孩子则找到最低的父节点且父节点的左孩子是node所在的子树
            //另一种说法：如果是右孩子则找到node所在子树被称为左子树的父节点
            else if (IsRight(node))
            {
                n = node.Parent;
                while (n != null)
                {
                    if (IsRight(n))
                        n = n.Parent;
                    else
                        return n.Parent;
                }
            }
            return null;
        }
        #endregion

        #region 前序遍历、中序遍历、后序遍历、层次遍历、Z形(蛇形)遍历

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="n"></param>
        public void Preorder(Node n)
        {
            if (n == null)
                return;
            Print(n);
            Preorder(n.L);
            Preorder(n.R);
        }

        /// <summary>
        /// 中序遍历
        /// </summary>
        /// <param name="n"></param>
        public void Inorder(Node n)
        {
            if (n == null)
                return;
            Inorder(n.L);
            Print(n);
            Inorder(n.R);
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <param name="n"></param>
        public void Postorder(Node n)
        {
            if (n == null)
                return;
            Postorder(n.L);
            Postorder(n.R);
            Print(n);
        }

        /// <summary>
        /// 层次遍历
        /// </summary>
        /// <param name="n"></param>
        public void Levelorder(Node n)
        {
            if (n == null)
                return;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(n);
            while(queue.Count>0)
            {
                var item = queue.Dequeue();
                if (item == null)
                    continue;
                Print(item);
                queue.Enqueue(item.L);
                queue.Enqueue(item.R);
            }
        }

        /// <summary>
        /// Z形(蛇形)遍历
        /// </summary>
        /// <param name="n"></param>
        public void ZLevelorder(Node n)
        {
            Stack<Node> stackl2r = new Stack<Node>();
            Stack<Node> stackr2l = new Stack<Node>();

            stackl2r.Push(n);

            while(stackl2r.Count>0|| stackr2l.Count > 0)
            {
                while (stackl2r.Count>0)
                {
                    n = stackl2r.Pop();
                    if (n == null)
                        continue;
                    Print(n);
                    stackr2l.Push(n.L);
                    stackr2l.Push(n.R);
                }

                while (stackr2l.Count > 0)
                {
                    n = stackr2l.Pop();
                    if (n == null)
                        continue;
                    Print(n);
                    stackl2r.Push(n.R);
                    stackl2r.Push(n.L);
                }
            }
        }
        #endregion

        #region 旋转 LL、LR、RR、RL

        /// <summary>
        /// LL旋转
        /// </summary>
        /// <returns>返回旋转后根节点</returns>
        Node LLRotation()
        {
            return LLRotation(Root);
        }

        /// <summary>
        /// LL旋转
        /// </summary>
        /// <param name="n">根节点</param>
        /// <returns>返回旋转后根节点</returns>
        Node LLRotation(Node n)
        {
            Node k1;
            Node k2 = n;

            k1 = k2.L;
            k2.L = k1.R;
            k1.R = k2;
            k2.H = MaxHeight(k2.L, k2.R)+1;
            k1.H= MaxHeight(k1.L, k1.R)+1;
            return k1;
            
        }

        /// <summary>
        /// RR旋转
        /// </summary>
        /// <returns>返回旋转后根节点</returns>
        Node RRRotation()
        {
            return RRRotation(Root);
        }

        /// <summary>
        /// RR旋转
        /// </summary>
        /// <param name="n">根节点</param>
        /// <returns>返回旋转后根节点</returns>
        Node RRRotation(Node n)
        {
            Node k1=n;
            Node k2 ;

            k2 = k1.R;
            k1.R = k2.L;
            k2.L = k1;
            
            k1.H = MaxHeight(k1.L, k1.R) + 1;
            k2.H = MaxHeight(k2.L, k2.R) + 1;
            return k2;
        }

        /// <summary>
        /// LR旋转
        /// </summary>
        /// <returns>返回旋转后根节点</returns>
        Node LRRotation()
        {
            return LRRotation(Root);
        }

        /// <summary>
        /// LR旋转
        /// </summary>
        /// <param name="n">根节点</param>
        /// <returns>返回旋转后根节点</returns>
        Node LRRotation(Node n)
        {
            Node k3=n;
            k3.L = RRRotation(k3.L);
            return LLRotation(k3);
        }

        /// <summary>
        /// RL旋转
        /// </summary>
        /// <returns>返回旋转后根节点</returns>
        Node RLRotation()
        {
            return RLRotation(Root);
        }

        /// <summary>
        /// RL旋转
        /// </summary>
        /// <param name="n">根节点</param>
        /// <returns>返回旋转后根节点</returns>
        Node RLRotation(Node n)
        {
            Node k1 = n;
            k1.R = LLRotation(k1.R);
            return RRRotation(k1);
        }

        #endregion

        #region 增、删、查
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Node Search(int key)
        {
            return Search(Root, key);
        }
        public Node Search(Node node,int key)
        {
            if(node == null|| node.Key==key)
            {
                return node;
            }

            if (node.Key > key)
                return Search(node.L,key);
            else
                return Search(node.R, key);
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="tree">AVL树的根结点</param>
        /// <param name="key">插入的结点的键值</param>
        /// <returns></returns>
        public Node Insert( Node tree, int key)
        {
            if(tree==null)
            {
                // 创建节点
                tree=CreateNode(key);
            }
            else if(key<Root) // 应该将key插入到"tree的左子树"的情况
            {
                tree.L = Insert(tree.L, key);
                // 插入节点后，若AVL树失去平衡，则进行相应的调节。
                if (Height(tree.L) - Height(tree.R) == 2)
                {
                    if (key < tree.L.Key)
                        tree = LLRotation(tree);
                    else
                        tree = LRRotation(tree);
                }

                
            }
            else if(key>Root) // 应该将key插入到"tree的右子树"的情况
            {
                tree.R = Insert(tree.R, key);
                // 插入节点后，若AVL树失去平衡，则进行相应的调节。
                if (Height(tree.R) - Height(tree.L) == 2)
                {
                    if(key>tree.L.Key)
                        tree = RRRotation(tree);
                    else
                        tree = RLRotation(tree);
                }

            }
            else // key ==n.Key
            {
                throw new System.Exception("不允许插入相同键值的节点");
            }
            tree.H = MaxHeight(tree.L, tree.R) + 1;
            return tree;
        }
        
        public Node Remove(Node n)
        {
            return Remove(Root, n);
        }

        public Node Remove(Node tree, Node n)
        {
            if (tree == null || n == null)
                return null;
            

            if(n<tree)  // 待删除的节点在"tree的左子树"中
            {
                tree.L = Remove(tree.L, n);
                // 删除节点后，若AVL树失去平衡，则进行相应的调节。
                if (Height(tree.R)-Height(tree.L)==2)
                {
                    Node k1 = tree.R;
                    if (Height(k1.L) < Height(k1.R))
                        tree = RRRotation(tree);
                    else
                        tree = RLRotation(tree);
                }


            }
            else if(n> tree) // 待删除的节点在"tree的右子树"中
            {
                tree.R = Remove(tree.R, n);
                // 删除节点后，若AVL树失去平衡，则进行相应的调节。
                if (Height(tree.L) - Height(tree.R) == 2)
                {
                    Node k1 = tree.L;
                    if (Height(k1.L) > Height(k1.R))
                        tree = LLRotation(tree);
                    else
                        tree = LRRotation(tree);
                }
            }
            else // tree节点就是要删除的节点
            {
                // tree的左右孩子都存在
                if (HasTwoChild(tree))
                {
                    if(Height(tree.L)>Height(tree.R))
                    {
                        // 如果tree的左子树比右子树高；
                        // 则(01)找出tree的左子树中的最大节点
                        //   (02)将该最大节点的值赋值给tree。
                        //   (03)删除该最大节点。
                        // 这类似于用"tree的左子树中最大节点"做"tree"的替身；
                        // 采用这种方式的好处是：删除"tree的左子树中最大节点"之后，AVL树仍然是平衡的。
                        Node max = Maximum(tree.L);
                        tree.Key = max.Key;
                        tree.L = Remove(tree.L, max);
                    }
                    else
                    {
                        // 如果tree的左子树不比右子树高(即它们相等，或右子树比左子树高1)
                        // 则(01)找出tree的右子树中的最小节点
                        //   (02)将该最小节点的值赋值给tree。
                        //   (03)删除该最小节点。
                        // 这类似于用"tree的右子树中最小节点"做"tree"的替身；
                        // 采用这种方式的好处是：删除"tree的右子树中最小节点"之后，AVL树仍然是平衡的。

                        Node min = Minimum(tree.R);
                        tree.Key = min.Key;
                        tree.R = Remove(tree.R, min);
                    }
                }
                else
                {
                    Node tmp = tree;
                    tree = tree.L!=null ? tree.L : tree.R;
                    tmp.Parent = null;
                    tmp.L = null;
                    tmp.R=null;
                }
            }

            return tree;
        }

        #endregion

        #region print
        public void Print(Node node)
        {
            Console.Write($"{node.Key},");
        }
        public static void PrintTree(Node tree)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            PrintTree(tree, builder);
            Console.WriteLine(builder.ToString());
        }
        
        static void PrintTree(Node tree,System.Text.StringBuilder builder)
        {
            List<List<Node>> list = new List<List<Node>>();
            Queue<Node> queue = new Queue<Node>();
            Queue<Node> queue1 = null;
            queue.Enqueue(tree);
            while(queue.Count>0)
            {
                List<Node> levels = new List<Node>();
                queue1 = new Queue<Node>();
                int nullCount = 0;
                while (queue.Count>0)
                {
                    Node n = queue.Dequeue();
                    Node l=n!=null?n.L:null, r= n != null ? n.R : null;
                    
                    levels.Add(n);
                    queue1.Enqueue(l);
                    queue1.Enqueue(r);
                    nullCount += l == null ? 1 : 0;
                    nullCount += r == null ? 1 : 0;
                }
                queue = queue1;
                list.Add(levels);
                if(queue.Count==nullCount)
                {
                    break;
                }
            }


            int level = list.Count;
            int maxLevelNodeCount = GetMaxNodeCountByDepth(level);
            int space=0;

            for (int i = level-1; i >=0; i--)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder top = new System.Text.StringBuilder();
                int space1 = space;
                space = space * 2 + 1;
                for (int s = 0; s < space1; s++)
                {
                    sb.Append("   ");
                    top.Append("   ");
                }
                List<Node> ls = list[i];
                for (int k = 0; k < ls.Count; k++)
                {
                    string v = ls[k] == null ? " N " : ls[k].Key.ToString("D3");
                    sb.Append($"{v}");
                    top.Append("/ \\");
                    for (int j = 0; j < space; j++)
                    {
                        sb.Append($"   ");
                        top.Append($"   ");
                    }
                }
                
                top.Append("\n");
                sb.Append("\n");
                
                if (i!=level-1)
                    builder.Insert(0, top.ToString());
                builder.Insert(0, sb.ToString());


            }
        }
        #endregion
        #region test

        static void Main()
        {
            AVLTree tree = new AVLTree();
            Node n,n1,n2;
            tree.Root= tree.CreateNode(6, null);
            n=tree.CreateLNode(1, tree.Root);
            n1=tree.CreateRNode(5, n);
            n1= tree.CreateLNode(3, n1);
            tree.CreateLNode(2, n1);
            tree.CreateRNode(4, n1);

            n2 =tree.CreateRNode(7, tree.Root);
            n2 = tree.CreateRNode(9, n2);
            tree.CreateLNode(8, n2);
            n = tree.CreateRNode(15, n2);
            tree.CreateLNode(12, n);
            tree.CreateRNode(18, n);
            n=tree.CreateLNode(10, n);
            n = tree.CreateRNode(17, n);

            tree.Preorder(tree.Root);
            Console.WriteLine();
            tree.Inorder(tree.Root);
            Console.WriteLine();
            tree.Postorder(tree.Root);
            Console.WriteLine();
            tree.Levelorder(tree.Root);
            Console.WriteLine("\n\n\n\n\n");
            PrintTree(tree.Root);
            Console.WriteLine("\n\n\n\n\n");
            
            Console.ReadKey();
        }

        #endregion
    }
}
