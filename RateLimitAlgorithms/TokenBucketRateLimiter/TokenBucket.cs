namespace RateLimitAlgorithms;

public class TokenBucket
{
    private readonly double _capacity;
    private readonly double _timeWindowInSeconds;
    private double _availableCapacity;
    private DateTime _lastRefillTime;
    private readonly double _refillCountPerSecond;

    public TokenBucket(double capacity, double timeWindowInSeconds)
    {
        _capacity = capacity;
        _timeWindowInSeconds = timeWindowInSeconds;
        _availableCapacity = capacity;
        _lastRefillTime = DateTime.Now;
        _refillCountPerSecond = capacity / _timeWindowInSeconds;
    }

    public double GetAvailableCapacity()
    {
        Refill();
        return _availableCapacity;
    }

    public bool Consume()
    {
        Refill();

        if (_availableCapacity > 0)
        {
            _availableCapacity -= 1;
            return true;
        }

        return false;
    }

    private void Refill()
    {
        var timeNow = DateTime.Now;
        var elapsedTime = Math.Abs((timeNow - _lastRefillTime).TotalSeconds);
        var tokenToAdd = (int)Math.Round(elapsedTime * _refillCountPerSecond);
        if (tokenToAdd >= 1)
        {
            _lastRefillTime = timeNow;
            _availableCapacity = Math.Min(_capacity, tokenToAdd + _availableCapacity);
        }
    }
}

