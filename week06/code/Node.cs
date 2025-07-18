public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // PROBLEM 1: Insert Unique Values Only
        if (value == Data)
        {
            return; // Duplicate value - do not insert
        }

        if (value < Data)
        {
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else // value > Data
        {
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // PROBLEM 2: Contains
        if (value == Data)
            return true;
        else if (value < Data)
            return Left?.Contains(value) ?? false;
        else // value > Data
            return Right?.Contains(value) ?? false;
    }

    public int GetHeight()
    {
        // PROBLEM 4: Tree Height
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}
