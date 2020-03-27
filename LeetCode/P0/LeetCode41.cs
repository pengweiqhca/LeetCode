using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.P0
{
    public class LeetCode41
    {
        public int FirstMissingPositive(int[] nums)
        {
            var min = 1;
            while (true)
            {
                var hasMatched = false;

                for (var index = 0; index < nums.Length; index++)
                {
                    if (nums[index] == min)
                    {
                        hasMatched = true;
                        min++;
                        break;
                    }
                }

                if (!hasMatched) return min;
            }
        }
    }
}
