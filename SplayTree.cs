using System;
using System.Collections.Generic;

namespace Codingriver_Splay
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

        public static bool operator !(Node a) //如果为null，则非null是真，null为假
        {
            if (a == null)
                return true;
            return false;
        }

        #endregion
    }
    #endregion



    /// <summary>
    /// 伸展树
    /// 伸展树无需时刻都严格地保持全树的平衡，但却能够在任何足够长的真实操作序列中，保持分摊意义上的高效率。伸展树也不需要对基本的二叉树节点结构，做任何附加的要求或改动，更不需要记录平衡因子或高度之类的额外信息，故适用范围更广。
    /// - 刚刚被访问过的元素，极有可能在不久之后再次被访问到
    /// - 将被访问的下一元素，极有可能就处于不久之前被访问过的某个元素的附近
    /// 伸展树可以不更新高度，高度在这里没有用
    /// </summary>
    public class SplayTree
    {
        //树的根节点
        public Node Root;

        #region create node
        public Node CreateNode(int key, Node parent = null, Node left = null, Node right = null)
        {
            Node n=new Node(key, parent, left, right);
            if (left != null)
                left.Parent = n;
            if (right != null)
                right.Parent = n;

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
            if (parent == null && null == x)
            {
                Root = null;
                return;
            }
            else if (parent == null) 
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
        public void AttachAsLChild(Node parent,Node x)
        {
            if (parent == null && null == x)
            {
                Root = null;
                return;
            }
            else if (parent == null)
            {
                Root = x;
                x.Parent = null;
                return;
            }
            else if (x == null)
            {
                parent.L = x;
                return;
            }
            if (parent == x)
                return;

            if (x == null) return;
            x.Parent = parent;
            parent.L = x;

        }
        public void AttachAsRChild(Node parent, Node x)
        {
            if (parent == null && null == x)
            {
                Root = null;
                return;
            }
            else if (parent == null)
            {
                Root = x;
                x.Parent = null;
                return;
            }
            else if (x == null)
            {
                parent.R = x;
                return;
            }
            if (parent == x)
                return;
            x.Parent = parent;
            parent.R = x;
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


        /// <summary>
        /// 伸展操作
        /// Zig:右旋转，顺时针旋转
        /// Zag:左旋转，逆时针旋转
        /// 
        ///  - 单旋(单层伸展)                                                                                                                                                                                                                                                
        ///              zig                              zag        
        ///        （顺时针旋转g）                   （逆时针旋转g）                                                                                                                                                                                                               
        ///               g                                g                                                                                                                                                                                                                                      
        ///              / \                              / \                                                                                                                                                                                                                                     
        ///             p   T3                           T0  p                                                                                                                                                                                                                                    
        ///            / \                                  / \                                                                                                                                                                                                                                   
        ///           v   T2                               T1  v                                                                                                                                                                                                                                  
        ///          / \                                      / \                                                                                                                                                                                                                                 
        ///         T0  T1                                   T2  T3     
        ///
        ///       zig单旋后：                          zag单旋后：                                                                                                                                                                                                      
        ///              p                                  p                                                                                                                                                                                                              
        ///           /     \                            /     \                                                                                                                                                                                                         
        ///          v       g                          g       v                                                                                                                                                                                                        
        ///         / \     / \                        / \     / \                                                                                                                                                                                                       
        ///        T0  T1  T2  T3                     T0  T1  T2  T3       
        /// 
        ///  - 双旋(双层伸展)
        ///                    zig-zig                               zig-zag                               zag-zag                                zag-zig
        ///       （先顺时针旋转g，后顺时针旋转p） （先顺时针旋转p(zig)，后逆时针旋转g(zag)）  （先逆时针旋转g，后逆时针旋转p）  （先逆时针旋转p(zag)，后顺时针旋转g(zig)）
        ///                      g                                     g                                      g                                      g                                                                                                                                                                                                                                       
        ///                     / \                                   / \                                    / \                                    / \                                                                                                                                                                                                          
        ///                    p   T3                               T0   p                                  T0  p                                  p   T3                                                                                                                                                                                                       
        ///                   / \                                       / \                                    / \                                / \                                                                                                                                                                             
        ///                  v   T2                                    v   T3                                 T1  v                              T0  v                                                                                                                                                                                                             
        ///                 / \                                       / \                                        / \                                / \                                                                                                                                                                                                          
        ///                T0  T1                                    T1  T2                                     T2  T3                             T1  T2                                                                                                                                                                               
        ///
        ///       双旋后：  zig-zig 双旋后                        zig-zag双旋后                        zag-zag 双旋后                          zag-zig双旋后                                                                                                                                                                                                 
        ///                      v                                      v                                     v                                       v                                                                                                                                                                             
        ///                     / \                                 /     \                                 / \                                   /     \                                                                                                                                                        
        ///                    T0   p                              g       p                               p   T3                                p       g                                                                                                                                                                                      
        ///                        / \                            / \     / \                             / \                                   / \     / \                                                                                                                                                                                                                                                                            
        ///                       T1  g                          T0  T1  T2  T3                          g   T2                                T0  T1  T2  T3                                                                                                                                                                                                                    
        ///                          / \                                                                / \                                                                                                                                                                                                                          
        ///                         T2  T3                                                             T0  T1                                                                                                                                                                                                                                                     
        ///
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Node Splay(Node v)
        {
            if (v==null) return null;
            Node g=null, p=null;

            while(!!(p=v.Parent)&& !!(g=p.Parent))//自下而上，反复对v做双层伸展
            {
                Node r = g.Parent;
                if(IsLeft(v))
                {
                    if(IsLeft(p)) //zig-zig   先旋转v的祖父节点，然后再旋转v的父节点
                    {
                        AttachAsLChild(g, p.R);AttachAsLChild(p, v.R);
                        AttachAsRChild(p, g);AttachAsRChild(v, p);
                    }
                    else //zig-zag   先旋转v的父节点，后旋转v的祖父节点
                    {
                        AttachAsLChild(p, v.R); AttachAsRChild(g, v.L);
                        AttachAsLChild(v, g); AttachAsRChild(v, p);
                    }
                }
                else
                {
                    if (IsRight(p))// zag-zag
                    {
                        AttachAsRChild(g, p.L); AttachAsRChild(p, v.L);
                        AttachAsLChild(p, g); AttachAsLChild(v,p);
                        
                    }
                    else  //zag-zig
                    {
                        AttachAsRChild(p, v.L); AttachAsLChild(g, v.R);
                        AttachAsRChild(v, g); AttachAsLChild(v, p);
                    }
                }

                if (r == null)//若原v的曾祖父r不存在，则v现在为树根Root
                {
                    v.Parent = null;
                }
                else
                {
                    if (g == r.L)
                        AttachAsLChild(r, v);
                    else
                        AttachAsRChild(r, v);
                }
                UpdateHeight(g);
                UpdateHeight(p);
                UpdateHeight(v);

            }//双层伸展结束时，必有g=null，但p可能非空

            if(p==v.Parent&&p!=null) //如果p为非空，则需要做一次单旋
            {
                if(IsLeft(v)) /* zig */
                {
                    AttachAsLChild(p, v.R);AttachAsRChild(v, p);
                }
                else /* zag */
                {
                    AttachAsRChild(p, v.L); AttachAsLChild(v, p); 
                }
                UpdateHeight(p);UpdateHeight(v);
            }

            v.Parent = null;
            return v;
        }


        #region 增、删、查
        public Node Insert(int key)
        {
            Console.WriteLine($"Insert:{key}"); //打印日志
            Root = Insert(Root, key);
            PrintTree(Root);//打印日志 打印树
            return Root;
        }
        public Node Insert(Node tree, int key)
        {
            if (Root == null)
                return Root = CreateNode(key);
            Node hot, root;
            hot=root = Search(tree, key);
            if (root == key)
                return root;
            if(hot<key)
            {
                root = CreateNode(key,null, hot, hot.R);
                hot.R = null;
            }
            else
            {
                root = CreateNode(key, null, hot.L, hot);
                hot.L = null;
            }
            //更新高度，也可以不更新，伸展树不用高度

            
            return root; //返回新节点位置

        }


        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        /// <returns>返回新的树根节点</returns>
        public Node Search(Node tree,int key)
        {
            Node hot;
            Node x=SearchIn(tree, key, out hot);
            Root = Splay(x != null ? x : hot); //最后x或者hot旋转到根节点，设置树根
            return Root;
        }
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
        Node Remove(Node tree, int key)
        {
            if (tree == null)
                return null;

            Node x = Search(tree, key);
            if (x != key)
                return null;
            x = RemoveAt(x);
            return x;
        }


        Node RemoveAt(Node x)
        {
            if (x == null)
            {
                return null;
            }
            Node succ = null;
            Node parent = x.Parent; //x为root节点，parent=null；
            if (!HasLeft(x))
                succ = x.R;
            else if (!HasRight(x))
                succ = x.L;
            else
            {
                succ = Successor(x);
                SwapData(x, succ);
                x = succ;
                succ = x.R; //succ = Successor(x) 这行执行前，succ是x的后继，而且succ是x右子树中的一个节点，所以succ是没有左孩子的，可能有右孩子，那么succ的后继只能是succ.Parent，继succ=x.Parent
                parent = x.Parent;
            }

            if (IsLeft(x)) x.Parent.L = null; //断开父节点的连接
            else if (IsRight(x)) x.Parent.R = null; //断开父节点的连接            
            Connect(parent, succ);
            x.L = x.R = x.Parent = null; //clean

            return x;
        }

        #endregion


        void SwapData(Node x, Node y)
        {
            int d = x.Key;
            x.Key = y.Key;
            y.Key = d;
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
            SplayTree tree = new SplayTree();
            Node n, n1, n2;
            tree.Insert(20);
            tree.Insert(10);
            tree.Insert(7);  
            tree.Insert(24);
            tree.Insert(26); 
            tree.Insert(12); 
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

            tree.Remove(24);
            tree.Remove(20);
            tree.Remove(10);
            tree.Remove(18); 
            tree.Remove(7);
            tree.Remove(26);
            tree.Remove(16);
            tree.Remove(12);


            Console.ReadKey();
        }

        #endregion
    }
}
