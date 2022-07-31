using System.Collections.Concurrent;

namespace RateLimitAlgorithms;

public class LeakyBucket
{
    private readonly ConcurrentQueue<DateTime> _queue;
    private readonly int _processWindow;
    private readonly int _capacity;

    public LeakyBucket(int capacity, int processWindow)
    {
        _queue = new();
        _capacity = capacity;
        _processWindow = processWindow;
    }

    public int GetProcessWindow()
    {
        return _processWindow;
    }

    public DateTime? Process()
    {
        var successful = _queue.TryDequeue(out DateTime data);

        return successful ? data : null;
    }

    public bool CanAccess()
    {
        if (_queue.Count < _capacity)
        {
            Console.Write(" -> processing this req");

            _queue.Enqueue(DateTime.Now);

            Console.WriteLine();

            return true;
        }

        Console.Write(" -> blocking this req as queue is full");

        Console.WriteLine();

        return false;
    }
}