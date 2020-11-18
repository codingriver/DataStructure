using System;

namespace Codingriver
{
    /// <summary>
    /// 插入排序(E版本)(优化版本)(稳定排序)
    /// 时间复杂度:
    ///     最优情况双层循环次数：内循环次数 i=1(1),i=2(1),...,i=n-2(1)，总次数=(n-2) 
    ///     最优情况每次比较都不需要交换，执行内循环一次时复杂度为1（比较一次），所以复杂度=2*(n-2),O=2(n-2)=O(n)
    /// </summary>
    public class InsertSort
    {
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void Sort<T>(T[] data) where T : IComparable
        {
            int len = data.Length;
            Sort(data, 0, len - 1);
        }
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="low">数组下标最小值</param>
        /// <param name="high">数组下标最大值</param>
        public static void Sort<T>(T[] data, int low, int high) where T : IComparable
        {
            T tmp;
            int j;
            for (int i = low+1; i <= high; i++) //第一个当做有序序列
            {
                tmp = data[i];
                for (j = i; j > 0 && data[j - 1].CompareTo(tmp) > 0; j--) //内循环使用冒泡方式对前面有序序列进行插入
                {
                    data[j] = data[j - 1];
                }
                data[j] = tmp;
            }


        }

    }
}
