using System;

namespace LeetCode.P6
{
    public class LeetCode665
    {
        public bool CheckPossibility(int[] nums)
        {
            if (nums.Length < 3) return true;

            var replaced = false;
            for (var index = 0; index < nums.Length - 1; index++)
            {
                if (nums[index] <= nums[index + 1]) continue;

                if (replaced) return false;

                if (index > 0 && index < nums.Length - 2)
                {
                    if (nums[index] > nums[index + 2]) nums[index] = nums[--index];
                    else nums[index + 1] = nums[index];
                }

                replaced = true;
            }

            return true;
        }
    }
}
