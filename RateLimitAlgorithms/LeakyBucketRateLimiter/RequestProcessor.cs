using System.Timers;

namespace RateLimitAlgorithms;

public class RequestProcessor
{
    private readonly System.Timers.Timer _timer;
    private readonly LeakyBucket _bucket;

    public RequestProcessor(LeakyBucket bucket)
    {
        _bucket = bucket;
        _timer = new System.Timers.Timer(_bucket.GetProcessWindow())
        {
            AutoReset = true
        };
        _timer.Elapsed += HandleRequest;
        _timer.Enabled = true;
    }

    private void HandleRequest(object? sender, ElapsedEventArgs e)
    {
        var data = _bucket.Process();

        if (data == null)
        {
            return;
        }

        Console.WriteLine($"processed data {data}");
    }
}

