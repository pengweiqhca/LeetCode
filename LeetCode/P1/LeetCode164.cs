using System;

namespace LeetCode.P1
{
    public class LeetCode164
    {
        public int MaximumGap(int[] nums)
        {
            Array.Sort(nums);

            var num = 0;

            for (var index = 0; index < nums.Length - 1; index++)
            {
                var diff = nums[index + 1] - nums[index];
                if (diff > num) num = diff;
            }

            return num;
        }
    }
}
