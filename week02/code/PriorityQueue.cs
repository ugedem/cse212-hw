using System;
using System.Collections.Generic;

public class PriorityQueue
{
    private readonly List<PriorityItem> _queue = new();

    public void Enqueue(string value, int priority)
    {
        _queue.Add(new PriorityItem(value, priority));
    }

    public string Dequeue()
    {
        if (_queue.Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        int bestIdx = 0;
        int bestPri = _queue[0].Priority;

        // Find highest priority; tie‑break by lower index (earlier enqueue)
        for (int i = 1; i < _queue.Count; i++)
        {
            if (_queue[i].Priority > bestPri)
            {
                bestPri = _queue[i].Priority;
                bestIdx = i;
            }
        }

        string result = _queue[bestIdx].Value;
        _queue.RemoveAt(bestIdx);
        return result;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value    { get; }
    internal int    Priority { get; }

    internal PriorityItem(string value, int priority)
    {
        Value    = value;
        Priority = priority;
    }

    public override string ToString() => $"{Value} (Pri:{Priority})";
}
