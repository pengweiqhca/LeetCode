using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.P0
{
    public class LeetCode42
    {
        public int Trap(int[] height)
        {
            int start = -1, end = -1, total = 0;

            for (var index = 0; index < height.Length; index++)
            {
                if (start < 0)
                {
                    if (height[index] > 0) start = index;
                }
                else if (height[index] >= height[start])
                {
                    for (var i = start + 1; i < index; i++)
                    {
                        total += height[start] - height[i];
                    }

                    start = index;
                }
            }

            if (start < 0) return total;

            for (var index = height.Length - 1; index >= start; index--)
            {
                if (end < 0)
                {
                    if (height[index] > 0) end = index;
                }
                else if (height[index] >= height[end])
                {
                    for (var i = index + 1; i < end; i++)
                    {
                        total += height[end] - height[i];
                    }

                    end = index;
                }
            }

            return total;
        }
    }
}
