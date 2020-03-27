using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.P0
{
    //https://leetcode-cn.com/problems/he-wei-sde-lian-xu-zheng-shu-xu-lie-lcof/
    public class LeetCode57II
    {
        public int[][] FindContinuousSequence(int target)
        {
            var max = (int)Math.Sqrt(2 * target);
            if (max * max == target * 2) max--;

            return target % 2 == 1 ? A1(target, max).ToArray() : A2(target, max).ToArray();
        }
        private IEnumerable<int[]> A1(int target, int max)
        {
            for (var num = max % 2 == 0 ? max - 1 : max; num >= 3; num--)
            {
                var index = target / num;
                if (target % num == 0)
                {
                    if (index % 2 == 1 && num % 2 == 1)
                        yield return FillArray(index - num / 2, num);
                }
                else if (target % num * 2 == num)
                    yield return FillArray(index - num / 2 + 1, num);
            }

            yield return Enumerable.Range(target / 2, 2).ToArray();
        }

        private int[] FillArray(int start, int num)
        {
            var array = new int[num];
            for (var index = 0; index < num; index++)
            {
                array[index] = start + index;
            }

            return array;
        }

        private IEnumerable<int[]> A2(int target, int max)
        {
            for (var num = max; num >= 3; num--)
            {
                var index = target / num;
                if (target % num == 0)
                {
                    if (num % 2 == 1 && index % 2 == 0)
                        yield return FillArray(index - num / 2, num);

                }
                else if (target % num * 2 == num)
                    yield return FillArray(index - num / 2 + 1, num);
            }
        }
    }
}
