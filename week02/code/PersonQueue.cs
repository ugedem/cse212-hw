// PersonQueue.cs
using System;
using System.Collections.Generic;

public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    public bool IsEmpty() => _queue.Count == 0;

    // Enqueue: add to the back
    public void Enqueue(Person person)
    {
        _queue.Add(person);
    }

    // Dequeue: remove from the front
    public Person Dequeue()
    {
        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}
