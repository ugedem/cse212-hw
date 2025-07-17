using System;

public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
            throw new InvalidOperationException("No one in the queue.");

        var person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // infinite turns: always re-enqueue
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // consume one turn and re-enqueue
            person.Turns--;
            _people.Enqueue(person);
        }
        // if Turns == 1: last turn, do NOT re-enqueue

        return person;
    }

    public override string ToString() => _people.ToString();
}
