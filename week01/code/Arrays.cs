using System;
using System.Collections.Generic;

public static class Arrays
{
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN: Steps to implement MultiplesOf function
        // 1. Create a new array of doubles with size equal to 'length'.
        // 2. Use a loop that starts at 0 and runs until length - 1.
        // 3. For each index i, calculate number * (i + 1) and store it in the array.
        // 4. Return the filled array.

        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN: Steps to implement RotateListRight function
        // 1. Get the last 'amount' of elements using GetRange.
        // 2. Get the remaining elements from the start to (count - amount).
        // 3. Clear the original list.
        // 4. Add the rotated (last part first, then the first part) back into the list.

        List<int> lastPart = data.GetRange(data.Count - amount, amount);
        List<int> firstPart = data.GetRange(0, data.Count - amount);

        data.Clear();
        data.AddRange(lastPart);
        data.AddRange(firstPart);
    }
}
