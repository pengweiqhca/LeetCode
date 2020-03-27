using System.Collections.Generic;
using System.Linq;

namespace LeetCode.P0
{
    public class LeetCode57
    {
        public int[][] Insert(int[][] intervals, int[] newInterval) => Insert2(intervals, newInterval).ToArray();

        private IEnumerable<int[]> Insert2(int[][] intervals, int[] newInterval)
        {
            var state = -1; //<0：未匹配；0：正在跨区匹配；>0：已匹配
            foreach (var interval in intervals)
            {
                if (state < 0)
                {
                    if (newInterval[0] < interval[0])
                    {
                        if (newInterval[1] < interval[0])
                        {
                            state = 1;
                            yield return newInterval; //当前区间左边
                            yield return interval;
                        }
                        else if (newInterval[1] <= interval[1])
                        {
                            state = 1;
                            yield return new[] { newInterval[0], interval[1] }; //左边合并
                        }
                        else state = 0;
                    }
                    else if (newInterval[0] <= interval[1])
                    {
                        if (newInterval[1] <= interval[1])
                        {
                            state = 1;
                            yield return interval; //被当前区间包含
                        }
                        else
                        {
                            state = 0;
                            newInterval[0] = interval[0]; //当前区间包含新区间起点
                        }
                    }
                    else yield return interval; //在当前区间右边
                }
                else if (state == 0)
                {
                    if (newInterval[1] < interval[0])
                    {
                        state = 1;
                        yield return newInterval; //当前区左边
                        yield return interval;
                    }
                    else if (newInterval[1] <= interval[1])
                    {
                        state = 1;
                        yield return new[] { newInterval[0], interval[1] }; //左边合并
                    }
                }
                else yield return interval;
            }

            if (state < 1) yield return newInterval;
        }
    }
}
