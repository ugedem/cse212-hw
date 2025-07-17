using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 – Test cases for PriorityQueue
[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with distinct priorities.
    // Expected Result: Dequeue returns items in descending priority order.
    // Defect(s) Found: Original Dequeue didn’t remove the highest‑priority item before returning it.
    public void TestPriorityQueue_DescendingPriorities()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("apple", 1);
        pq.Enqueue("banana", 3);
        pq.Enqueue("cherry", 2);

        // 1) Highest priority 3 → "banana"
        Assert.AreEqual("banana", pq.Dequeue(), 
            "First Dequeue should return 'banana' (priority 3).");

        // 2) Next priority 2 → "cherry"
        Assert.AreEqual("cherry", pq.Dequeue(), 
            "Second Dequeue should return 'cherry' (priority 2).");

        // 3) Lowest priority 1 → "apple"
        Assert.AreEqual("apple", pq.Dequeue(), 
            "Third Dequeue should return 'apple' (priority 1).");
    }

    [TestMethod]
    // Scenario: Enqueue items [first, second, middle, third] with priorities [5,5,10,5].
    // Expected Result: 
    //   • Dequeue returns "middle" first (highest priority 10), 
    //   • then among the remaining priority-5 items, returns in FIFO: first, second, third.
    // Defect(s) Found: Original Dequeue failed to honor FIFO for equal priorities.
    public void TestPriorityQueue_FIFOforTies()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("first", 5);
        pq.Enqueue("second", 5);
        pq.Enqueue("middle", 10);
        pq.Enqueue("third", 5);

        Assert.AreEqual("middle", pq.Dequeue(), 
            "First Dequeue should return 'middle' (priority 10).");

        Assert.AreEqual("first", pq.Dequeue(), 
            "Second Dequeue should return 'first' (priority 5).");
        Assert.AreEqual("second", pq.Dequeue(), 
            "Third Dequeue should return 'second' (priority 5).");
        Assert.AreEqual("third", pq.Dequeue(), 
            "Fourth Dequeue should return 'third' (priority 5).");
    }

    [TestMethod]
    // Scenario: Dequeuing from an empty queue.
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None once fixed.
    public void TestPriorityQueue_EmptyThrows()
    {
        var pq = new PriorityQueue();
        var ex = Assert.ThrowsException<InvalidOperationException>(
            () => pq.Dequeue(),
            "Dequeuing empty queue should throw InvalidOperationException."
        );
        Assert.AreEqual("The queue is empty.", ex.Message,
            "Exception message must be exactly 'The queue is empty.'");
    }

    [TestMethod]
    // Scenario: Interleaved Enqueue and Dequeue calls.
    //   1) Enqueue A(1), B(2) → Dequeue() should return B.
    //   2) Enqueue C(3) → Dequeue() should return C.
    //   3) Dequeue() should return A.
    // Expected Result: [B, C, A]
    // Defect(s) Found: None once fixed.
    public void TestPriorityQueue_InterleavedOperations()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 1);
        pq.Enqueue("B", 2);

        Assert.AreEqual("B", pq.Dequeue(), 
            "First Dequeue should return 'B' (priority 2).");

        pq.Enqueue("C", 3);
        Assert.AreEqual("C", pq.Dequeue(), 
            "Second Dequeue should return 'C' (priority 3).");

        Assert.AreEqual("A", pq.Dequeue(), 
            "Third Dequeue should return 'A' (priority 1).");
    }
}
