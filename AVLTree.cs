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
        public int H=1; //height;

        public Node(int key, Node parent=null, Node left=null, Node right=null,int h=1)
        {
            Key = key;
            Parent = parent;
            L = left;
            R = right;
            H = h;
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
        public static bool operator ==(Node a, int key)
        {
            if (a is null )
            {
                return false;
            }

            return a.Key == key;
        }
        public static bool operator ==(int key,Node a)
        {
            if (a is null)
            {
                return false;
            }

            return a.Key == key;
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
        public static bool operator !=(Node a, int key)
        {
            if (a is null)
            {
                return false;
            }

            return a.Key != key;
        }
        public static bool operator !=(int key, Node a)
        {
            if (a is null)
            {
                return false;
            }

            return a.Key != key;
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
            Node n=new Node(key, parent, left, right);
            if (parent > n) parent.L = n;
            else if(parent<n) parent.R = n;
            return n;
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
        /// 兄弟
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Node Sibling(Node n)
        {
            return IsLeft(n) ? n.Parent.R : n.Parent.L;
        }

        /// <summary>
        /// 叔叔
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Node Uncle(Node n)
        {
            return IsLeft(n.Parent) ? n.Parent.Parent.R : n.Parent.Parent.L;
        }

        /// <summary>
        /// 建立父子关系
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="x"></param>
        public void Connect(Node parent,Node x)
        {
            if (parent == null) 
            {
                Root = x;
                x.Parent = null;
                return;
            }
            if (parent == x)
                return;
                
            if (x == null) return;
            x.Parent = parent;
            if (x > parent) parent.R = x;
            else parent.L = x;
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
        /// 树的高度为最大层次。即空的二叉树的高度是0，非空树的高度等于它的最大层次(根的层次为1，根的子节点为第2层，依次类推)，这里空树的高度取0，有的教材资料是-1
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

        public int UpdateHeight(Node a)
        {
            a.H = MaxHeight(a.L, a.R) + 1;
            return a.H;
        }

        /// <summary>
        /// 是否平衡
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public bool IsBalanced(Node tree)
        {
            return -2 < Height(tree.L) - Height(tree.R) && Height(tree.L) - Height(tree.R) < 2;
        }


        /// <summary>
        /// 在左、右孩子中取更高者
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public Node TallerChild(Node tree)
        {
            if (tree == null)
                return null;
            if (Height(tree.L) > Height(tree.R))
                return tree.L;
            else return tree.R;
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

        /// <summary>
        /// 查找键值key的节点并且返回（key对应节点为命中节点）
        /// </summary>
        /// <param name="tree">子树</param>
        /// <param name="key">查找键值</param>
        /// <param name="hot">返回命中节点的父节点，如果不存在命中节点则返回预判命中节点的父节点；（当命中节点为tree时，hot为NULL）</param>
        /// <returns>命中节点</returns>
        public Node SearchIn(Node tree, int key, out Node hot)
        {
            // hot :如果没有命中节点，则hot命中后做多只有一个节点，且key是对应另外一个孩子空节点位置

            Node n=tree;
            if (tree == key) //在子树根节点tree处命中
            {
                hot = null;
                return tree;
            }

            for(; ; )
            {
                hot = n;
                n = n > key ? n.L : n.R;
                if (n==null||n == key) return n; //返回命中节点，hot指向父节点，hot必然命中（在key不存在时,n==null）
            }
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

        #region 旋转 

        /// <summary>
        /// 左旋转，逆时针旋转,单旋
        /// </summary>
        /// <param name="tree">非空孙辈节点</param>
        /// <returns>该树新的的根节点</returns>
        public Node Zag(Node tree)
        {
            Node v = tree, p = v.Parent, g = p.Parent, r = g.Parent;
            Node a = g, b = p, c = v;
            Node T0 = g.L, T1 = p.L, T2 = v.L, T3 = v.R;
            a.L = T0; if (T0 != null) T0.Parent = a;
            a.R = T1; if (T1 != null) T1.Parent = a; UpdateHeight(a);
            c.L = T2; if (T2 != null) T2.Parent = c;
            c.R = T3; if (T3 != null) T3.Parent = c; UpdateHeight(c);
            b.L = a; a.Parent = b;
            b.R = c; c.Parent = b; UpdateHeight(b);
            Connect(r, b); //根节点和父节点联接
            return b;//该树新的的根节点
        }

        /// <summary>
        /// 右旋转，顺时针旋转,单旋
        /// </summary>
        /// <param name="tree">非空孙辈节点</param>
        /// <returns>该树新的的根节点</returns>
        public Node Zig(Node tree)
        {
            Node v = tree, p = v.Parent, g = p.Parent, r = g.Parent;
            Node a = v, b = p, c = g;
            Node T0 = v.L, T1 = v.R, T2 = p.R, T3 = g.R;
            a.L = T0; if (T0 != null) T0.Parent = a;
            a.R = T1; if (T1 != null) T1.Parent = a; UpdateHeight(a);
            c.L = T2; if (T2 != null) T2.Parent = c;
            c.R = T3; if (T3 != null) T3.Parent = c; UpdateHeight(c);
            b.L = a; a.Parent = b;
            b.R = c; c.Parent = b; UpdateHeight(b);

            Connect(r, b); //根节点和父节点联接
            return b;//该树新的的根节点
        }

        /// <summary>
        /// 双旋转，先tree的父节点Zag后对tree的祖父Zig，先对p左旋，后对g右旋
        /// </summary>
        /// <param name="tree">非空孙辈节点</param>
        /// <returns>该树新的的根节点</returns>
        public Node ZigZag(Node tree)
        {
            Node v = tree, p = v.Parent, g = p.Parent, r = g.Parent;
            Node a = p, b = v, c = g;
            Node T0 = p.L, T1 = v.L, T2 = v.R, T3 = g.R;
            a.L = T0; if (T0 != null) T0.Parent = a;
            a.R = T1; if (T1 != null) T1.Parent = a; UpdateHeight(a);
            c.L = T2; if (T2 != null) T2.Parent = c;
            c.R = T3; if (T3 != null) T3.Parent = c; UpdateHeight(c);
            b.L = a; a.Parent = b;
            b.R = c; c.Parent = b; UpdateHeight(b);
            Connect(r, b); //根节点和父节点联接
            return b;//该树新的的根节点
        }

        /// <summary>
        /// 双旋转，先Zig后Zag，先对p右旋，后对g左旋
        /// </summary>
        /// <param name="tree">非空孙辈节点</param>
        /// <returns>该树新的的根节点</returns>
        public Node ZagZig(Node tree)
        {
            Node v = tree, p = v.Parent, g = p.Parent, r = g.Parent;
            Node a = g, b = v, c = p;
            Node T0 = g.L, T1 = v.L, T2 = v.R, T3 = p.R;
            a.L = T0; if (T0 != null) T0.Parent = a;
            a.R = T1; if (T1 != null) T1.Parent = a; UpdateHeight(a);
            c.L = T2; if (T2 != null) T2.Parent = c;
            c.R = T3; if (T3 != null) T3.Parent = c; UpdateHeight(c);
            b.L = a; a.Parent = b;
            b.R = c; c.Parent = b; UpdateHeight(b);
            Connect(r, b); //根节点和父节点联接
            return b;//该树新的的根节点
        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        Node RotateAt(Node r)
        {
            Node g = r, p = TallerChild(g), v = TallerChild(p);
            if (IsLeft(p))
            {
                if (IsLeft(v)) return Zig(v);
                else return ZigZag(v);

            }
            else
            {
                if (IsRight(v)) return Zag(v);
                else return ZagZig(v);
            }
        }



        #endregion

        #region 增、删、查
        public Node Insert(int key)
        {
            Console.WriteLine($"Insert:{key}"); //打印日志
            Node n = Insert(Root, key);
            PrintTree(Root);//打印日志 打印树
            return n;
        }
        public Node Insert(Node tree, int key)
        {
            if (Root == null)
                return Root = CreateNode(key);

            Node hot;
            Node x = SearchIn(tree, key, out hot);
            if (x != null) return x;
            x = CreateNode(key, hot); //hot最多只有一个节点

            for (Node n = hot; n != null; n = n.Parent) // //从x之父出发向上，逐层检查各代祖先g
            {
                if (!IsBalanced(n)) //一旦发现g失衡，则（采用“3 + 4”算法）使之复衡，并将子树
                {
                    RotateAt(n);
                    break; //g复衡后，局部子树高度必然复原；其祖先亦必如此，故调整随即结束
                }
                else  //否则（g依然平衡），只需简单地
                    UpdateHeight(n); //更新其高度（注意：即便g未失衡，高度亦可能增加）
            } // 至多只需一次调整；若果真做过调整，则全树高度必然复原
            return x; //返回新节点位置

        }


        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Node Search(Node tree,int key)
        {
            return SearchIn(tree, key, out _);
        }


        #endregion







        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Node Remove(int key)
        {
            Console.WriteLine($"Remove:{key}"); //打印日志
            Node n = Remove(Root, key);
            PrintTree(Root);//打印日志 打印树
            return n;
        }
        Node Remove(Node tree,int key)
        {
            Node hot;
            Node x = SearchIn(tree, key, out hot);
            x = RemoveAt(x, out hot);

            for (Node n = hot; n != null; n = n.Parent) // //从x之父出发向上，逐层检查各代祖先g
            {
                if (!IsBalanced(n)) //一旦发现g失衡，则（采用“3 + 4”算法）使之复衡，并将子树
                {
                    RotateAt(n);
                    break; //g复衡后，局部子树高度必然复原；其祖先亦必如此，故调整随即结束
                }
                else  //否则（g依然平衡），只需简单地
                    UpdateHeight(n); //更新其高度（注意：即便g未失衡，高度亦可能增加）
            } // 至多只需一次调整；若果真做过调整，则全树高度必然复原
            return x;
        }
        void SwapData(Node x,Node y)
        {
            int d = x.Key;
            x.Key = y.Key;
            y.Key = d;
        }

        Node RemoveAt(Node x,out Node hot)
        {
            Node succ=null;
            Node parent = x.Parent;
            if (!HasLeft(x))
                succ = x.L;
            else if (!HasRight(x))
                succ = x.R;
            else
            {
                succ = Successor(x);
                SwapData(x, succ);
                x = succ;
                succ = x.Parent; //succ = Successor(x) 这行执行前，succ是x的后继，而且succ是x右子树中的一个节点，所以succ是没有左孩子的，可能有右孩子，那么succ的后继只能是succ.Parent，继succ=x.Parent
                parent = x.Parent;                
            }
            
            Connect(parent, succ);
            hot = parent;
            if (IsLeft(x))  x.Parent.L = null; //断开父节点的连接
            else            x.Parent.R = null; //断开父节点的连接
            x.L = x.R = x.Parent = null; //clean
            
            return x;
        }


        #region print
        public void Print(Node node)
        {
            Console.Write($"{node.Key},");
        }
        /// <summary>
        /// 打印二叉树 打印数值最大3位数
        /// </summary>
        /// <param name="tree">树的根节点</param>
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
            tree.Insert(20);
            tree.Insert(10);
            tree.Insert(7);
            tree.Insert(24);
            tree.Insert(26);
            tree.Insert(12);
            tree.Insert(14);
            tree.Insert(16);
            tree.Insert(13);
            tree.Insert(17);
            tree.Insert(18);
            Console.Write("   Preorder::");
            tree.Preorder(tree.Root);
            Console.WriteLine();
            Console.Write("    Inorder::");
            tree.Inorder(tree.Root);
            Console.WriteLine();
            Console.Write("  Postorder::");
            tree.Postorder(tree.Root);
            Console.WriteLine();
            Console.Write(" Levelorder::");
            tree.Levelorder(tree.Root);
            Console.WriteLine();
            Console.Write(" ZLevelorder:");
            tree.ZLevelorder(tree.Root);
            Console.WriteLine("\n\n");

            tree.Remove(14);
            tree.Remove(12);
            tree.Remove(10);
            tree.Remove(7);
            tree.Remove(26);
            tree.Remove(17);

            Console.ReadKey();
        }

        #endregion
    }
}
