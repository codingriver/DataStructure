using System;
using System.Collections.Generic;

namespace Codingriver
{
    /// <summary>
    /// 十大经典排序算法
    /// </summary>
    class SortingAlgorithm
    {
        const int MAX_RANDOM_VALUE = 99; //测试数组的最大数值
        /// <summary>
        /// 冒泡排序（A版本）
        /// 从后往前扫描待排序序列，如果前一个元素比后一个元素大，就交换它们两个，对每一对相邻元素作同样的工作；这样，第一次扫描待排序的序列会找到一个最小值并将其放置在第一位，第二次扫描待排序的序列会找到一个第二小的值并将其放置在第二位，第三次扫描待排序的序列会找到一个第三小的值并将其放置在第三位，以此类推，直到将所有元素排序完毕；排序的过程就像泡泡不断的往上冒，总是小的泡泡在最上面，大的泡泡在最下面。
        /// 时间复杂度：
        ///     双层循环次数：内循环次数 i=0(n-1),i=1(n-2),i=2(n-3),...,i=n-3(2),i=n-2(1)为等差数列，总次数=n*(0+n-1)/2=n*(n-1)/2
        ///     假设每次比较都需要交换，执行内循环一次时复杂度为2（比较一次+交换一次），所以复杂度=2*n(n-1)/2=n(n-1)
        ///     当n非常大时，多项式以幂次方最大的为标准所以复杂度O=n(n-1)=O(n*n)
        /// </summary>
        /// <param name="A"></param>
        void BubbleSort(int[]A)
        {
            int n = A.Length;
            for (int i = 0; i < n-1; i++)
            {
                for (int j = 0; j < n-1-i; j++)
                {
                    if(A[j]>A[j+1])
                    {
                        Swap(ref A[j + 1],ref A[j]);
                    }
                }
            }
        }

        /// <summary>
        /// 冒泡排序（E版本）(最优版本)
        /// 时间复杂度:
        ///     最优的时间复杂度：当数据本身是有序的时候，只会比较但是不会交换，内循环执行一圈就结束了，复杂度O=n-1=O(n)
        ///     最坏的时间复杂度：O=n(n-1)=O(n*n)
        /// </summary>
        /// <param name="A"></param>
        void BubbleSort_E(int[] A)
        {
            int n = A.Length;
            bool sorted = false; //整体排序标志，首先假定尚未排序
            while(!sorted)
            {
                sorted = true;//假定有序
                for (int i = 0; i < n-1; i++)
                {
                    if (A[i] > A[i + 1])
                    {
                        Swap(ref A[i + 1], ref A[i]);
                        sorted = false;
                    }
                }
                n--;//因整体排序不能保证，需要清除排序标志

            }
        }

        /// <summary>
        /// 选择排序(直接选择排序)
        /// 一次从待排序的序列中选出最小（或最大）的一个元素，存放在已排好序的序列的后一个位置，直到全部待排序的数据元素排完；
        /// 时间复杂度:
        ///     双层循环次数：内循环次数 i=0(n),i=1(n-1),i=2(n-2),...,i=n-2(2),i=n-1(1)为等差数列，总次数=(n-1)*(0+n)/2=n*(n-1)/2
        ///     最坏情况每次比较都需要交换，执行内循环一次时复杂度为2（比较一次+交换一次），所以复杂度=2*n(n-1)/2=n(n-1),O=n(n-1)=O(n*n)
        ///     最优情况每次比较都不需要交换,执行内循环一次时复杂度为1（比较一次），，所以复杂度=n(n-1)/2,O=n(n-1)/2=O(n*n)
        /// </summary>
        /// <param name="A"></param>
        void SelectionSort(int[] A)
        {
            int n = A.Length;
            int min;
            for (int i = 0; i < n-1 ; i++)
            {
                min = i;
                for (int j = i; j < n ; j++)
                {
                    if (A[min] > A[j])
                    {
                        min = j ;
                    }
                }
                Swap(ref A[min], ref A[i]);
                //Console.Write($"{i},{min}"); PrintArray(A, i, min+1);
            }
        }

        /// <summary>
        /// 插入排序(直接插入排序)
        /// 把待排序的记录按其关键码值的大小逐个插入到一个已经排好序的有序序列中，直到所有的记录插入完为止，得到一个新的有序序列。
        /// 时间复杂度:
        ///     最坏情况双层循环次数：内循环次数 i=1(1),i=2(2),...,i=n-2(n-2)为等差数列，总次数=(n-2)*(1+n-2)/2=(n-2)*(n-1)/2
        ///     最坏情况每次比较都需要交换，执行内循环一次时复杂度为2（比较一次+交换一次），所以复杂度=2*(n-2)*(n-1)/2,O=(n-1)(n-2)=O(n*n)
        /// </summary>
        /// <param name="A"></param>
        void InsertionSort(int[] A)
        {
            int n = A.Length;
            for (int i = 1; i < n-1; i++) //第一个当做有序序列
            {
                for (int j = i; j >0&&A[j-1]>A[j]; j--) //内循环使用冒泡方式对前面有序序列进行插入
                {
                    Swap(ref A[j - 1], ref A[j]);
                }
            }
        }
        /// <summary>
        /// 插入排序(E版本)(优化版本)
        /// 时间复杂度:
        ///     最优情况双层循环次数：内循环次数 i=1(1),i=2(1),...,i=n-2(1)，总次数=(n-2) 
        ///     最优情况每次比较都不需要交换，执行内循环一次时复杂度为1（比较一次），所以复杂度=2*(n-2),O=2(n-2)=O(n)
        /// </summary>
        /// <param name="A"></param>
        void InsertionSort_E(int[] A)
        {
            int n = A.Length;
            int j,tmp;
            for (int i = 1; i < n - 1; i++) //第一个当做有序序列
            {
                tmp = A[i];
                for (j = i; j > 0 && A[j - 1] > tmp; j--) //内循环使用冒泡方式对前面有序序列进行插入
                {
                    A[j] = A[j - 1];
                }
                A[j] = tmp;
            }
        }

        /// <summary>
        /// 希尔排序
        /// 先取一个小于n的整数d1作为第一个增量，把数组元素分组，所有距离为d1的倍数的记录放在同一个组中，先在各组内进行直接插入排序；然后，取第二个增量d2<d1重复上述的分组和排序，直至所取的增量  =1(  <  …<d2<d1)，即所有记录放在同一组中进行直接插入排序为止。>
        /// 时间复杂度:(推算不出来，如果有小伙伴推算出来欢迎解说指点)
        ///    参考其他资料,复杂度和递增序列h有关（increment sequence）
        ///    By combining the arguments of these two theorems h-sequences with O(log(n)) elements can be derived that lead to a very good performance in practice, as for instance the h-sequence of the program (Sedgewick [Sed 96]). But unfortunately, there seems to be no h-sequence that gives Shellsort a worst case performance of O(n·log(n)) (see [Sed 96]). It is an open question whether possibly the average complexity is in O(n·log(n))
        /// </summary>
        /// <param name="A"></param>
        void ShellSort(int[]A)
        {
            int n = A.Length;
            int h = 1;
            while(h<3)
            {
                h = h * 3 + 1;
            }

            while(h>=1)
            {
                for (int i = h; i < n; i++)
                {
                    for (int j = i; j >= h && A[j] < A[j - h]; j -= h)
                    {
                        if(A[j-h]>A[j])
                        {
                            Swap(ref A[j - h], ref A[j]);
                        }
                    }
                }
                h = h / 3;
            }

        }

        /// <summary>
        /// 归并排序
        /// 首先两个子序列分别是有序的（递归后最小数组长度为1，认为数组长度为1时数组本身是有序的），这里对两个子序列合并，挑选两个子序列中最小的放入reg临时序列中，直到两个子序列中一个子序列被完全放入后结束，然后将另一个子序列复制到reg临时序列中，最后临时序列是合并后的有序序列了，将reg复制到A中
        /// 时间复杂度：假设递归一次的时间复杂度为T()
        ///             执行1次递归的时间复杂度为T（n）=2*T(n/2)+n(两个子序列合并，一共长度为n)
        ///             执行2次递归的时间复杂度为T（n）=4*T(n/2)+2n
        ///             执行3次递归的时间复杂度为T（n）=8*T(n/8)+3n
        ///             类似二叉树的层数，层级=log2(n)+1
        ///             代入得T(n)=nT(1)+log2(n)*n
        ///             时间复杂度O=T(n)=nT(1)+log2(n)*n=O(nlog2(n))=(nlogn)
        ///             
        /// </summary>
        /// <param name="A"></param>
        public void MergeSort(int[]A)
        {
            int n = A.Length;
            int[] reg = new int[n];
            MergeSort(A, reg, 0, n - 1); 
        }

        void MergeSort(int[] A,int[] reg,int start,int end)
        {
            if(start>=end)
            {
                return;
            }
            int mid = (start + end) >> 1;
            int start1 = start;
            int end1 = mid;
            int start2 = mid + 1;
            int end2 = end;
            MergeSort(A, reg, start1, end1);
            MergeSort(A, reg, start2, end2);
            int k = start;
            //首先两个子序列分别是有序的，这里对两个子序列合并，挑选两个子序列中最小的放入reg临时序列中，直到两个子序列中一个子序列被完全放入后结束，然后将另一个子序列复制到reg临时序列中，最后临时序列是合并后的有序序列了，复制会A中
            while (start1<=end1&&start2<=end2) 
            {
                reg[k++] = A[start1] < A[start2] ? A[start1++] : A[start2++]; //
            }
            while(start1<=end1)
            {
                reg[k++] = A[start1++];
            }
            while (start2 <= end2)
            {
                reg[k++] = A[start2++];
            }
            Array.Copy(reg, start, A, start,end - start + 1);

        }

        /// <summary>
        /// 归并排序(非递归版)
        /// </summary>
        /// <param name="A"></param>
        public void MergeSort_E(int[] A)
        {
            int n = A.Length;
            int[] a = A;
            int[] b = new int[n];
            int seg, start;
            for (seg = 1; seg < n; seg += seg)
            {
                for (start = 0; start < n; start += seg + seg)
                {
                    int low = start, mid = Math.Min(start + seg, n), high = Math.Min(start + seg + seg, n);
                    int k = low;
                    int start1 = low, end1 = mid;
                    int start2 = mid, end2 = high;
                    while (start1 < end1 && start2 < end2)
                        b[k++] = a[start1] < a[start2] ? a[start1++] : a[start2++];
                    while (start1 < end1)
                        b[k++] = a[start1++];
                    while (start2 < end2)
                        b[k++] = a[start2++];
                }
                Array.Copy(b,  a, n);
            }
        }

        /// <summary>
        /// 快速排序
        /// 简单说是给基准数找正确索引位置的过程.
        /// 快速排序是对冒泡排序的一种改进。
        /// 首先选取一个初始值（一般选取待排序序列的第一个值），通过一趟排序将待排序序列分成两个子序列，使左子序列的所有数据都小于这个初始值，右子序列的所有数据都大于这个初始值，然后再按此方法分别对这两个子序列进行排序，递归的进行上面的步骤，直至每一个数据项都有如下性质：该数据项左边的数据都小于它，右边的数据都大于它，这样，整个序列就有序了。
        /// 时间复杂度：O=O(nlogn)和归并排序推理类似，不再展开推理了
        ///     
        /// </summary>
        /// <param name="A"></param>
        public void QuickSort(int[]A)
        {
            int n = A.Length;
            QuickSort(A, 0, n - 1);
        }

       
        void QuickSort(int[] A,int low,int high)
        {
            if (low >= high) return;
            int pivot = Partition(A, low, high);
            QuickSort(A, low, pivot - 1);
            QuickSort(A, pivot + 1,high);
        }
        

        int Partition(int[] A, int low, int high)
        {
            int pivot = A[low]; //基准数选取数组第一个元素（哨兵元素）
            while (low<high)
            {
                while (low < high && A[high] >= pivot) --high;
                A[low] = A[high];
                while (low < high && A[low] <= pivot) ++low;
                A[high] = A[low];
            }
            A[low] = pivot;
            return low;
        }

        /// <summary>
        /// 堆排序
        /// 堆排序（Heapsort）是指利用堆这种数据结构所设计的一种排序算法。
        /// 堆排序可以说是一种利用堆的概念来排序的选择排序。
        /// 堆的性质：
        ///     堆积是一个近似完全二叉树的结构，并同时满足堆积的性质：即子结点的键值或索引总是小于（或者大于）它的父节点。
        ///     -->大顶堆：每个节点的值都大于或等于其子节点的值，在堆排序算法中用于升序排列；
        ///     -->小顶堆：每个节点的值都小于或等于其子节点的值，在堆排序算法中用于降序排列；
        /// 堆排序的基本思想是：将待排序序列构造成一个大顶堆，此时，整个序列的最大值就是堆顶的根节点。将其与末尾元素进行交换，此时末尾就为最大值。然后将剩余n-1个元素重新构造成一个堆，这样会得到n个元素的次小值。如此反复执行，便能得到一个有序序列了
        /// 时间复杂度：(参考：https://blog.csdn.net/qq_34228570/article/details/80024306/)
        ///             构建初始堆复杂度：O(n)
        ///             交换并重建堆复杂度O(nlogn)
        ///             真个过程的复杂度O=O(n)+O(nlogn)=O(nlogn)
        ///             
        ///             
        /// </summary>
        /// <param name="A"></param>
        public void HeapSort(int[]A)
        {
            int n = A.Length;
            int i;
            // 初始化构建堆结构，i從最後一個父節點開始調整(n/2-1为二叉树倒数第二层最后一个父节点) 
            //构建后的二叉树根节点为整个二叉树中最大的节点
            for (i = n / 2 - 1; i >= 0; i--) //构建堆结构（完全二叉树，大顶堆）
                MaxHeapify(A, i, n - 1);
            for (i = n-1; i >0; i--)
            {
                Swap(ref A[0], ref A[i]);
                MaxHeapify(A, 0, i - 1);
            }
        }

        void MaxHeapify(int[]A,int start,int end)
        {
            // 建立父節點指標和子節點指標
            int dad = start;
            int son = dad * 2 + 1;
            while(son<=end)// 若子節點指標在範圍內才做比較
            {
                if (son + 1 < end && A[son] < A[son + 1]) son++; // 先比較兩個子節點大小，選擇最大的
                if (A[dad] > A[son]) return;//如果父節點大於子節點代表調整完畢，直接跳出函數
                else
                {   // 否則交換父子內容再繼續子節點和孫節點比較
                    Swap(ref A[dad], ref A[son]);
                    dad = son;son = dad * 2 + 1;
                }
            }

        }

        /// <summary>
        /// 计数排序
        /// 计数排序不是一个比较排序算法
        /// 计数排序的核心在于将输入的数据值转化为键存储在额外开辟的数组空间中。作为一种线性时间复杂度的排序，计数排序要求输入的数据必须是有确定范围的整数。
        /// 计数排序类似与桶排序，也是用空间换取了时间，计数排序要求数组必须在一个确定的区间内。
        /// 过程1：1. 首先找出数组的最大值和最小值；2. 遍历数组，以数字作为键，该数字出现的次数作为值插入哈希表中；3. 在最小值到最大值这个区间内遍历哈希表，将数字反向插入数组中。
        /// 过程2：
        ///         根据待排序集合中最大元素和最小元素的差值范围，申请额外空间；
        ///         遍历待排序集合，将每一个元素出现的次数记录到元素值对应的额外空间内；
        ///         对额外空间内数据进行计算，得出每一个元素的正确索引位置；
        ///         将待排序集合每一个元素移动到计算得出的正确索引位置上。
        /// 时间复杂度：
        ///             如果原始数列的规模是n，最大最小整数的差值是m，由于代码中第1、2、4步都涉及到遍历原始数列，运算量都是n，第3步遍历统计数列，运算量是m，所以总体运算量是3n+m，去掉系数，时间复杂度是O(n+m)。
        /// 
        /// 空间复杂度：
        ///             如果不考虑结果数组，只考虑统计数组的话，空间复杂度是O(m)
        /// 计数排序的局限性：
        ///                 当数组最大和最小差值过大时，并不适合计数排序
        ///                 当数组元素不是整数(不能转化成整数计算的，浮点(用指数和浮点分部转化)、字符等等)时，也不适合用计数排序
        /// </summary>
        /// <param name="A"></param>
        public void CountingSort(int[]A)
        {
            int n = A.Length;
            int[] sorting = new int[n];
            //1.找出数组中最大值、最小值
            int max = int.MinValue;
            int min = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                max = Math.Max(max, A[i]);
                min = Math.Min(min, A[i]);
            }
            //初始化计数数组count，设长度为m
            int[] counting = new int[max - min + 1];
            //2. 对计数数组各元素赋值，设长度为m
            for (int i = 0; i < n; i++)
                counting[i - min]++;

            //3. 计数数组变形，新元素的值是前面元素累加之和的值
            for (int i = 1; i < counting.Length; i++)
                counting[i] += counting[i - 1];
            //4. 遍历A中的元素，填充到结果数组中去，从后往前遍历
            for (int i = n-1; i >=0; i--)
                sorting[--counting[A[i] - min]] = A[i];
            //5. 将结果复制到原始数组中
            Array.Copy(sorting, A, n);

        }



        /// <summary>
        /// 桶排序（Bucket Sort）(箱排序)
        /// 桶排序是计数排序的扩展版本，计数排序可以看成每个桶只存储相同元素，而桶排序每个桶存储一定范围的元素，通过映射函数，将待排序数组中的元素映射到各个对应的桶中，对每个桶中的元素进行排序，最后将非空桶中的元素逐个放入原序列中。
        /// 算法过程：
        ///          根据待排序集合中最大元素和最小元素的差值范围和映射规则，确定申请的桶个数；
        ///          遍历待排序集合，将每一个元素移动到对应的桶中；
        ///          对每一个桶中元素进行排序，并移动到已排序集合中。
        /// 时间复杂度：设桶内比较排序为快速排序(复杂度nlogn)
        ///             第一个循环为O(N),设桶的数量为M，平均每个桶的元素数量为N/M,桶内以快速排序为例为NlogN，复杂度为O(M*N/M*log2(N/M)+N)=O(N+N(log2(N)-log2(M)))
        ///             第二个循环为O(2N)
        ///             总复杂度为O(3N+N(log2(N)-log2(M)))=O(N+N(logN-LogM))
        ///             当M=N时 复杂度为O(N)
        ///             当M=1时 复杂度为O(N+Nlog(N))
        ///这里桶内排序使用的是链表指针方式，桶内复杂度为O(1)，可以忽略，总复杂度为O(N)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="bucketCount"></param>
        public void BucketSort(int[]A, int bucketCount = 10)
        {
            int n = A.Length;
            int index;
            LinkedList<int>[] bucket = new LinkedList<int>[bucketCount];
            for (int i = 0; i < n; i++)
            {
                index = A[i] / bucketCount;
                if (bucket[index] == null)
                    bucket[index] = new LinkedList<int>();
                BucketInsertSort(bucket[index], A[i]);
            }
            index = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                LinkedList<int> linkedList = bucket[i];
                if (linkedList == null) continue;
                var current = linkedList.First;
                while(current!=null)
                {
                    A[index++] = current.Value;
                    current = current.Next;
                }
            }
        }
        /// <summary>
        /// 桶排序的桶内排序，这里用的是指针选择排序，还可使用快速排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="a"></param>
        void BucketInsertSort(LinkedList<int>list,int a)
        {
            var current = list.First;
            if(current==null||current.Value>a)
            {
                list.AddFirst(a);
                return;
            }
            while(current!=null)
            {
                if(current.Next==null||current.Next.Value>a)
                {
                    list.AddAfter(current,a);
                    return;
                }
                current = current.Next;
            }
            


        }

        /// <summary>
        /// 基数排序
        /// 概念1：基数排序是一种非比较型整数排序算法，其原理是将整数按位数切割成不同的数字，然后按每个位数分别比较。由于整数也可以表达字符串（比如名字或日期）和特定格式的浮点数，所以基数排序也不是只能使用于整数。
        /// 概念2：将所有待排序的数统一为相同的数位长度，数位较短的数前面补零，然后从低位到高位按位比较，位数字小的排在前面，大的排在后面，这样当比较第N位时前N-1位都是有序的，如此循环的比较，直到最高位比较完成，整个序列就是有序的了。
        /// 时间复杂度：
        ///             设待排序列为n个记录，序列中最大值的位数为d，数字的基数为 r，则进行链式基数排序的时间复杂度为O(d(n+r))。当分配数字时要对每一个数字进行按位比较， 而收集数字时要进行r次收集（如十进制数字就要进行从0到9共10次收集操作）， 故一趟分配时间复杂度为O(n)，一趟收集时间复杂度为O(r)，共进行d趟分配和收集。
        /// 
        /// 基数排序 vs 计数排序 vs 桶排序
        /// 这三种排序算法都利用了桶的概念，但对桶的使用方法上有明显差异：
        /// 基数排序：根据键值的每位数字来分配桶；
        /// 计数排序：每个桶只存储单一键值；
        /// 桶排序：每个桶存储一定范围的数值；
        /// </summary>
        /// <param name="A"></param>
        public void RadixSort(int[]A)
        {
            
            int n = A.Length;
            const int BASE = 10;
            int exp = 1;
            int max = int.MinValue;
            int[] tmp = new int[n];
            for (int i = 0; i < n; i++)
                if (A[i] > max) max = A[i];

            while (max/exp>0)
            {
                int[] bucket = new int[n];

                for (int i = 0; i < n; i++)
                {
                    bucket[A[i] / exp % BASE]++;
                }

                for (int i = 1; i < n; i++)
                {
                    bucket[i] += bucket[i - 1];
                }
                for (int i = n-1; i >=0; i--)
                {
                    tmp[--bucket[A[i]/exp%BASE]] = A[i];
                }
                Array.Copy(tmp, A, n);
                exp *= BASE;
            }


        }


        /// <summary>
        /// 交换
        /// 当a和b引用相同的地址空间时不适用，会出现结果为0，因为a和b是同一个变量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void Swap_E(ref int a,ref int b )
        {
            a = a + b;b = a - b;a = a - b;
        }
        /// <summary>
        /// 交换
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static void Main(string[] args)
        {
            SortingAlgorithm algorithm = new SortingAlgorithm();
            int[] A = RandomArray(15);
            PrintArray(A,0,A.Length);
            //algorithm.BubbleSort(A);
            //algorithm.BubbleSort_E(A);
            //algorithm.SelectionSort(A);
            //algorithm.InsertionSort(A);
            //algorithm.InsertionSort_E(A);
            //algorithm.ShellSort(A);
            //algorithm.MergeSort(A);
            //algorithm.MergeSort_E(A);
            //algorithm.QuickSort(A);
            //algorithm.HeapSort(A);
            //algorithm.BucketSort(A);
            algorithm.RadixSort(A);
            PrintArray(A, 0, A.Length);
            Console.ReadKey();
        }

        static int[] RandomArray(int count)
        {
            Random random = new Random();
            int[] A = new int[count];
            for (int i = 0; i < count; i++)
            {
                A[i] = random.Next(MAX_RANDOM_VALUE);
            }
            return A;
        }

        static void PrintArray(int[]A,int start,int end)
        {
            end = A.Length < end ? A.Length : end;
            start = start < 0 ? 0 : start;
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < start; i++)
            {
                builder.Append("   ,");
            }
            for (int i = start; i < end; i++)
            {
                builder.AppendFormat("{0},",A[i]);
            }
            Console.WriteLine(" ------> {0}", builder.ToString());
        }
        
    }
}
