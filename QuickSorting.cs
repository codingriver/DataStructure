using System;
using System.Collections.Generic;

namespace Codingriver
{
    /// <summary>
    /// 快速排序
    /// 简单说是给基准数找正确索引位置的过程.
    /// 快速排序是对冒泡排序的一种改进。
    /// 首先选取一个初始值（一般选取待排序序列的第一个值），通过一趟排序将待排序序列分成两个子序列，使左子序列的所有数据都小于这个初始值，右子序列的所有数据都大于这个初始值，然后再按此方法分别对这两个子序列进行排序，递归的进行上面的步骤，直至每一个数据项都有如下性质：该数据项左边的数据都小于它，右边的数据都大于它，这样，整个序列就有序了。
    /// 时间复杂度：O=O(nlogn)和归并排序推理类似，不再展开推理了
    /// 当数据长度小于等于20或者分割后小于等于20时自动使用插入排序
    /// </summary>
    public class QuickSorting
    {

        /// <summary>
        /// 快速排序（递归版）（不稳定排序）(优先使用)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void Sort<T>(T[] data) where T : IComparable
        {
            int len = data.Length;
            Sort(data, 0, len - 1);
        }
        private static void Sort<T>(T[] data, int low, int high) where T : IComparable
        {
            if (low >= high) return;
            if(high-low+1<=20)
            {
                InsertSort.Sort(data,low,high);
                return;
            }

            int split = Partition(data, low, high);
            Sort(data, low, split - 1);
            Sort(data, split + 1, high);
        }

        private static int Partition<T>(T[] data,int low,int high) where T : IComparable
        {
            T pivot = data[low];
            while (low<high)
            {
                while (low < high && data[high].CompareTo(pivot) >= 0) --high;
                data[low] = data[high];
                while (low < high && data[low].CompareTo(pivot) <= 0) ++low;
                data[high] = data[low];
            }
            data[low] = pivot;
            return low;
        }


        /// <summary>
        /// 快速排序（非递归版）（不稳定排序）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void Sort_V<T>(T[] data) where T : IComparable
        {
            Stack<int> stack = new Stack<int>(data.Length/2);
            T pivot;
            int low = 0;
            int high = data.Length - 1;
            int start, end;
            stack.Push(high);
            stack.Push(low);

            while (stack.Count > 0)
            {
                start = low = stack.Pop();
                end = high = stack.Pop();

                if (low >= high)
                    continue;
                if (high - low + 1 <= 20)
                {
                    InsertSort.Sort(data, low, high);
                    continue;
                }

                pivot = data[low];
                while (low < high)
                {
                    while (low < high && data[high].CompareTo( pivot)>=0) high--;
                    data[low] = data[high];

                    while (low < high && data[low].CompareTo( pivot)<=0) low++;
                    data[high] = data[low];
                }
                data[low] = pivot;
                stack.Push(low - 1);
                stack.Push(start);
                stack.Push(end);
                stack.Push(low + 1);

            }
        }

    }

}


