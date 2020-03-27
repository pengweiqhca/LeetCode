namespace LeetCode.P0
{
    public class LeetCode2
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null || l2 == null) return l1 ?? l2;

            var carry = 0;
            ListNode head = null, current = null;
            while (l1 != null && l2 != null)
            {
                var value = l1.val + l2.val + carry;
                if (value > 9)
                {
                    value %= 10;
                    carry = 1;
                }
                else
                    carry = 0;

                if (head == null)
                {
                    current = head = new ListNode(value);
                }
                else
                {
                    current = current.next = new ListNode(value);
                }

                l1 = l1.next;
                l2 = l2.next;
            }

            if (l1 == null) l1 = l2;

            while (l1 != null && carry > 0)
            {
                if (l1.val < 9)
                {
                    current = current.next = new ListNode(l1.val + carry);

                    carry = 0;
                }
                else
                {
                    current = current.next = new ListNode(0);
                }

                l1 = l1.next;
            }

            if (l1 != null)
            {
                current.next = l1;
            }
            else if (carry > 0)
            {
                current.next = new ListNode(1);
            }

            return head;
        }
    }
}
