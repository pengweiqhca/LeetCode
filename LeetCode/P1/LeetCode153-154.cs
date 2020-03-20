namespace LeetCode.P1
{
    public class LeetCode153//154
    {
        public int FindMin(int[] nums)
        {
            foreach (var num in nums)
                if (nums[0] > num) return num;

            return nums[0];
        }
    }
}
