namespace LeetCode.P6
{
    public class LeetCode605
    {
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            var start = -2;
            var index = 0;
            for (; index < flowerbed.Length; index++)
            {
                if (flowerbed[index] == 0) continue;

                n -= (index - start) / 2 - 1;

                start = index;
            }

            if (index > 0 && index == flowerbed.Length && index - start > 2)
                n -= (index - start - 1) / 2;

            return n <= 0;
        }
    }
}
