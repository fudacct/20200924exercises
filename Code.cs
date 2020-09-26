
#region 第四题代码
class Solution
{
    public ListNode mergeTwoLists(ListNode l1, ListNode l2)
    {
        if (l1 == null) return l2;
        else if (l2 == null) return l1;
        else if (l1.val < l2.val)
        {
            l1.next = mergeTwoLists(l1.next, l2);
            return l1;
        }
        else
        {
            l2.next = mergeTwoLists(l2.next, l1);
            return l2;
        }
    }

    public void Print(ListNode node)
    {
        Console.WriteLine(node.val);
        if (node.next == null)
            return;
        else
        {
            Print(node.next);
        }
    }
}
    #endregion

