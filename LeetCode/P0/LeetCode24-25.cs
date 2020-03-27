namespace LeetCode.P0
{
    public class LeetCode25
    {
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode h = null, e = null;
            while (true)
            {
                var node = Reverse(head, k, out var list, out var next);
                if (node == null) return h ?? head;

                if (h == null)
                {
                    h = list;
                }
                else
                {
                    e.next = list;
                }

                e = node;

                node.next = head = next;
            }
        }

        private static ListNode Reverse(ListNode list, int deep, out ListNode head, out ListNode next)
        {
            if (list == null) return next = head = null;

            if (deep == 1)
            {
                next = list.next;

                return head = list;
            }

            var node = Reverse(list.next, deep - 1, out head, out next);
            if (node == null) return null;

            node = node.next = list;
            node.next = null;
            return node;
        }
    }
}
