using System.Collections.Generic;
using System.Linq;

namespace LeetCode.P1
{
    public class LeetCode162
    {
        public int FindPeakElement(int[] nums)
        {
            if (nums.Length == 0) return -1;

            return FindPeakElements(nums).First();
        }

        public IEnumerable<int> FindPeakElements(int[] nums)
        {
            if (nums.Length < 2)
            {
                if (nums.Length == 1) yield return 0;

                yield break;
            }

            for (var index = 0; index < nums.Length; index++)
            {
                if (index == 0)
                {
                    if (nums[index + 1] < nums[index]) yield return index++;
                }
                else if (index == nums.Length - 1)
                {
                    if (nums[index - 1] < nums[index]) yield return index;
                }
                else if (nums[index - 1] < nums[index] &&
                         nums[index + 1] < nums[index]) yield return index++;
            }
        }
    }
}
