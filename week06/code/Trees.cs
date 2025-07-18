public static class Trees
{
    /// <summary>
    /// Given a sorted list (sortedNumbers), create a balanced BST.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Insert the middle value into the BST and recurse on left/right halves
    /// without slicing arrays.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        if (first > last)
            return;

        int mid = (first + last) / 2;
        bst.Insert(sortedNumbers[mid]);

        InsertMiddle(sortedNumbers, first, mid - 1, bst);  // Left half
        InsertMiddle(sortedNumbers, mid + 1, last, bst);   // Right half
    }
}
