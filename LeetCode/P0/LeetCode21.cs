namespace LeetCode.P0
{
    public partial class LeetCode21
    {
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode head = null, current = null;

            while (true)
            {
                if (l1 == null && l2 == null) break;

                var min = l1;
                if (min == null || l2 != null && min.val > l2.val)
                {
                    min = l2;
                    l2 = l2.next;
                }
                else
                {
                    l1 = l1.next;
                }

                if (head == null) current = head = new ListNode(min.val);
                else current = current.next = new ListNode(min.val);
            }

            return head;
        }
    }
}
