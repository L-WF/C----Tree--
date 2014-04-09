using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 快排
{
    class Tree
    {
        private int _data;
        private Tree _leftChild;
        private Tree _rightChild;

        public Tree(int value)
        {
            this._data = value;
            this._leftChild = null;
            this._rightChild = null;
        }

        public int data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public Tree leftChild
        {
            get { return this._leftChild; }
            set { this._leftChild = value; }
        }

        public Tree rightChild
        {
            get { return this._rightChild; }
            set { this._rightChild = value; }
        }
    }

    class text
    {
        private static int dataCount = 100; //数据量

        static Tree comTree = new Tree(0);  //完全二叉树的根节点
        static Tree sortTree = new Tree(0); //排序二叉树的根节点
        static List<Tree> comList = new List<Tree>();   
        static int[] ranData = new int[dataCount];
        static int comLeafCount = 0;
        static int sortLeafCount = 0;

        static void Main(string[] args)
        {
            Random ran = new Random();
            for(int i=0;i<dataCount;i++)
            {
                ranData[i] = ran.Next(1, 200);
            }
            do
            {
                switch (menu())
                {
                    case 1:
                        {
                            comTree = new Tree(0);
                            buildComTree();
                            Console.WriteLine("完全二叉树构建完成！按任意键继续...\n");
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            sortTree = new Tree(0);
                            buildSortTree();
                            Console.WriteLine("排序二叉树构建完成！按任意键继续...\n");
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            if(sortTree.data==0)
                            {
                                Console.WriteLine("排序二叉树尚未被构建！按任意键继续...\n");
                            }
                            else
                            {
                                InOrder(sortTree);
                                Console.WriteLine();
                                Console.WriteLine("\n中序序列输出完毕！按任意键继续...\n");
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            if (sortTree.data != 0)
                            {
                                sortLeafCount = 0;
                                countSortLeaf(sortTree);
                                Console.WriteLine("leafCount:" + sortLeafCount + "\n");
                            }
                            if ( comTree.data != 0 )
                            {
                                comLeafCount = 0;
                                countComLeaf(comTree);
                                Console.WriteLine("leafCount:" + comLeafCount + "\n");
                            }
                            Console.WriteLine("输出完毕！按任意键继续...\n");
                            Console.ReadKey();
                            break;
                        }
                    case 5:
                        {
                            if (sortTree.data != 0)
                            {
                                int leafDepth = 0;
                                leafDepth = depth(sortTree);
                                Console.WriteLine("sortTreeDepth:" + leafDepth + "\n");
                            }
                            if (comTree.data != 0)
                            {
                                int leafDepth = 0;
                                leafDepth = depth(comTree);
                                Console.WriteLine("comTreeDepth:" + leafDepth + "\n");
                            }
                            Console.WriteLine("输出完毕！按任意键继续...\n");
                            Console.ReadKey();
                            break;
                        }
                    default: return; 
                }
            } while (true);
            
        }

        /// <summary>
        /// 显示菜单，提示用户输入，处理输入并作为返回值
        /// </summary>
        static int menu()
        {
            int selected;
            Console.WriteLine("\n\n                                       菜单\n\n");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n");
            Console.WriteLine("1 .生成完全二叉树\n");
            Console.WriteLine("2 .生成二叉排序树\n");
            Console.WriteLine("3 .输出中序遍历序列\n");
            Console.WriteLine("4 .计算叶子结点数\n");
            Console.WriteLine("5 .计算二叉树深度\n");
            Console.WriteLine("6 .退出程序\n");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n\n");
            do
            {
                try
                {
                    selected = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("请输入正确的选项！\n");
                    continue;
                }
                    if(selected>=1 && selected<=7)
                    {
                        return selected;
                    }
                    else
                    {
                        Console.WriteLine("请输入正确的选项！\n");
                    }
            } while (true);
        }

        /// <summary>
        /// 创建完全二叉树
        /// </summary>
        static void buildComTree()
        {
            comList.Clear();
            comTree.data = ranData[0];
            comList.Add(comTree);
            for(int i=1;i<dataCount;i++)
            {
                Tree comTree2 = new Tree(ranData[i]);
                comList.Add(comTree2);
                if(i%2==0)
                {
                    comList[(i-1)/2].rightChild = comTree2;
                }
                else
                {
                    comList[i/2].leftChild = comTree2;
                }
            }
        }

        /// <summary>
        /// 创建排序二叉树
        /// </summary>
        static void buildSortTree()
        {
            sortTree.data = ranData[0];
            for(int i=1;i<dataCount;i++)
            {
                Tree newSort = new Tree(ranData[i]);
                Tree temp = sortTree;
                while(true)
                {
                    if(temp.data<=newSort.data)
                    {
                        if (temp.rightChild == null)
                        {
                            temp.rightChild = newSort;
                            break;
                        }
                        else
                        {
                            temp = temp.rightChild;
                        }
                    }
                    else
                    {
                        if (temp.leftChild == null)
                        {
                            temp.leftChild = newSort;
                            break;
                        }
                        else
                        {
                            temp = temp.leftChild;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算完全二叉树的叶子数
        /// </summary>
        static void countComLeaf(Tree root)
        {
            if (root != null)
            {
                if (root.leftChild == null && root.rightChild == null)
                {
                    comLeafCount++;
                }
                else
                {
                    if (root.leftChild != null)
                    {
                        countComLeaf(root.leftChild);
                    }
                    if (root.rightChild != null)
                    {
                        countComLeaf(root.rightChild);
                    }
                }
            }
        }

        /// <summary>
        /// 计算排序二叉树的叶子数
        /// </summary>
        static void countSortLeaf(Tree root)
        {
            if (root != null)
            {
                if (root.leftChild == null && root.rightChild == null)
                {
                    sortLeafCount++;
                }
                else
                {
                    if (root.leftChild != null)
                    {
                        countSortLeaf(root.leftChild);
                    }
                    if (root.rightChild != null)
                    {
                        countSortLeaf(root.rightChild);
                    }
                }
            }
        }

        /// <summary>
        /// 计算二叉树的深度，并作为返回值
        /// </summary>
        static int depth(Tree root)
        {
            int treeDepth = 0;
            if(root!=null)
            {
                int depthLeft = depth(root.leftChild);
                int depthRight = depth(root.rightChild);
                treeDepth = 1 + (depthLeft >= depthRight ? depthLeft : depthRight);
            }
            return treeDepth;
        }

        /// <summary>
        /// 用以输出中序序列
        /// </summary>
        /// <param name="root"></param>
        static void InOrder(Tree root)
        {
            if (root != null)
            {
                InOrder(root.leftChild);
                Console.Write(root.data+" ");
                InOrder(root.rightChild);
            }
        }
    }
}
