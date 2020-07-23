using System;
using System.Collections.Generic;

namespace Codingriver
{
    #region Node
    public class Node
    {
        public int Key;
        public Node Parent;//parent
        public Node L; //left
        public Node R; //right

        public Node() { }
        public Node(int key)
        {
            Key = key;
        }
        public Node(int key, Node parent, Node left=null, Node right=null)
        {

        }
        public static Node operator++(Node a)
        {
            a.Key++;
            return a;
        }
        public static Node operator --(Node a)
        {
            a.Key--;
            return a;
        }
        public static bool operator !(Node a)
        {
            return a.Key==0;
        }
        public static Node operator ~(Node a)
        {
            a.Key = ~a.Key;
            return a;
        }
        public static Node operator &(Node a,Node b)
        {
            Node n = new Node();
            n.Key = a.Key&b.Key;
            return n;
        }
        public static Node operator |(Node a, Node b)
        {
            Node n = new Node();
            n.Key = a.Key | b.Key;
            return n;
        }


        #region 二元运算符
        public static Node operator +(Node a,Node b)
        {
            Node n = new Node();
            n.Key = a.Key + b.Key;
            return n;
        }
        public static Node operator -(Node a, Node b)
        {
            Node n = new Node();
            n.Key = a.Key - b.Key;
            return n;
        }
        public static Node operator *(Node a, Node b)
        {
            Node n = new Node();
            n.Key = a.Key * b.Key;
            return n;
        }
        public static Node operator /(Node a, Node b)
        {
            Node n = new Node();
            n.Key = a.Key / b.Key;
            return n;
        }
        public static Node operator %(Node a, Node b)
        {
            Node n = new Node();
            n.Key = a.Key % b.Key;
            return n;
        }
        #endregion

        #region 比较运算符
        public static bool operator >(Node a, Node b)
        {
            if (a is null || b is null)
            {
                return false;
            }
            return a.Key > b.Key;
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
    /// 
    /// 二叉树排序（Binary Tree Sort）
    /// 二叉树排序是构建在二叉排序树（Binary Sort Tree）上的算法，二叉排序树或者是一棵空树，或者是具有下列性质的二叉树：
    ///     若左子树不空，则左子树上所有结点的值均小于或等于它的根结点的值；
    ///     若右子树不空，则右子树上所有结点的值均大于或等于它的根结点的值；
    ///     左、右子树也分别为二叉排序树。
    /// </summary>
    public class BSTree
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
            return n!=null&& n.L != null;
        }
        public bool HasRight(Node n)
        {
            return n != null && n.R != null;
        }
        public int GetDepth(Node n)
        {
            if (n == null)
                return 0;
            if (n == Root)
                return 1;
            return GetDepth(n.Parent) + 1;
        }

        public int GetHeight(Node n)
        {
            if (n == null)
                return 0;
            if (n == Root)
                return 1;
            return GetHeight(n.Parent) + 1;
        }

        public Node Maximum(Node n)
        {
            if (n == null)
                return null;
            while (n.R != null)
                n = n.R;
            return n;
        }
        public Node Minimum(Node n)
        {
            if (n == null)
                return null;
            while (n.L != null)
                n = n.L;
            return n;
        }

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
        /// <param name="key"></param>
        /// <returns></returns>
        public Node Insert(int key)
        {
            return Insert(CreateNode(key));
        }

        public Node Insert(Node node)
        {
            if (Root == null)
            {
                Root = node;
                return Root;
            }
            Node n = Root;
            Node x =null;
            while(n!=null)
            {
                x = n;
                if (node > n)
                    n = n.R;
                else if (node < n)
                    n = n.L;
                else
                {
                    n = n.L;  //允许插入相同键值，如果不允许注释该行，将return注释取消
                    //return n;
                }
                    
            }
            node.Parent = x;
            if (node > x)
                x.R = node;
            else
                x.L = node;
            return node;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Node Remove(Node n)
        {
            Node x=null;

            //1. 没有左右子节点
            if (!HasChild(n))
            {
                if (IsLeft(n))
                    n.Parent.L = null;
                else
                    n.Parent.R = null;
            }
            //2. 存在左节点或者右节点，删除后需要对子节点移动
            else if (HasOneChild(n))
            {
                x = n.L == null ? n.R : n.L;
                if (IsLeft(n))
                    n.Parent.L = x;
                else
                    n.Parent.R = x;
            }
            //3. 同时存在左右子节点，通过和后继节点交换后转换为前两种情况（后继节点不可能存在左孩子，有可能有右孩子；）
            else
            {
                //找到后继节点，将后继节点填到删除节点位置，(继节点不可能有左孩子，可能右孩子，也就是变为删除后继节点问题，且后继节点最多有一个孩子节点（右孩子）)
                Node successorNode = Successor(n); //找到删除节点n的后继节点，后继节点不可能存在左孩子，有可能有右孩子
                Node rightNode = successorNode.R;
                if(rightNode!=null)
                {
                    rightNode.Parent = successorNode.Parent;
                }
                if (successorNode.Parent != null)
                    if (IsLeft(successorNode))
                        successorNode.Parent.L = rightNode;
                    else
                        successorNode.Parent.R = rightNode;
                successorNode.Parent = n.Parent;
                if (IsLeft(n))
                    n.Parent.L = successorNode;
                else if (IsRight(n))
                    n.Parent.R = successorNode;
                else
                    Root = successorNode;
                successorNode.L = n.L;
                if (n.L != null)
                    n.L.Parent = successorNode;
                successorNode.R = n.R;
                if (n.R != null)
                    n.R.Parent = successorNode;
            }
            
            n.L = null;
            n.R = null;
            n.Parent = null;
            return n;
        }

        #endregion


        public void Print(Node node)
        {
            Console.Write($"{node.Key},");
        }

        #region test

        static void Main()
        {
            BSTree tree = new BSTree();
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
            tree.CreateLNode(10, n);


            tree.Preorder(tree.Root);
            Console.WriteLine();
            tree.Inorder(tree.Root);
            Console.WriteLine();
            tree.Postorder(tree.Root);
            Console.WriteLine();
            tree.Levelorder(tree.Root);

            Console.WriteLine();
            tree.ZLevelorder(tree.Root);
            Console.WriteLine();
            //tree.Remove(tree.Find(9));
            //tree.Preorder(tree.Root);
            //Console.WriteLine();
            //tree.Inorder(tree.Root);
            //Console.WriteLine();
            //tree.Postorder(tree.Root);
            Console.WriteLine();
            Console.WriteLine($" 2 Predecessor:{tree.Predecessor(tree.Search(2)).Key}");
            Console.WriteLine($" 3 Predecessor:{tree.Predecessor(tree.Search(3)).Key}");
            Console.WriteLine($" 4 Predecessor:{tree.Predecessor(tree.Search(4)).Key}");
            Console.WriteLine($" 5 Predecessor:{tree.Predecessor(tree.Search(5)).Key}");
            Console.WriteLine($" 6 Predecessor:{tree.Predecessor(tree.Search(6)).Key}");
            Console.WriteLine($" 7 Predecessor:{tree.Predecessor(tree.Search(7)).Key}");
            Console.WriteLine($" 8 Predecessor:{tree.Predecessor(tree.Search(8)).Key}");
            Console.WriteLine($" 9 Predecessor:{tree.Predecessor(tree.Search(9)).Key}");
            Console.WriteLine($"10 Predecessor:{tree.Predecessor(tree.Search(10)).Key}");
            Console.WriteLine($" 1 Predecessor:{tree.Predecessor(tree.Search(1))}");
            Console.WriteLine();
            Console.WriteLine($" 1 Successor:{tree.Successor(tree.Search(1)).Key}");
            Console.WriteLine($" 2 Successor:{tree.Successor(tree.Search(2)).Key}");
            Console.WriteLine($" 3 Successor:{tree.Successor(tree.Search(3)).Key}");
            Console.WriteLine($" 4 Successor:{tree.Successor(tree.Search(4)).Key}");
            Console.WriteLine($" 5 Successor:{tree.Successor(tree.Search(5)).Key}");
            Console.WriteLine($" 6 Successor:{tree.Successor(tree.Search(6)).Key}");
            Console.WriteLine($" 7 Successor:{tree.Successor(tree.Search(7)).Key}");
            Console.WriteLine($" 8 Successor:{tree.Successor(tree.Search(8)).Key}");
            Console.WriteLine($" 9 Successor:{tree.Successor(tree.Search(9)).Key}");
            Console.WriteLine($"10 Successor:{tree.Successor(tree.Search(10))}"); 
            Console.WriteLine("\n\n\n\n\n");
            
            Console.ReadKey();
        }

        #endregion
    }
}
