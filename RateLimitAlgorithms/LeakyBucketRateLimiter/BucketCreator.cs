namespace RateLimitAlgorithms;

public class BucketCreator
{
    private readonly Dictionary<string, LeakyBucket> bucket;
    public BucketCreator()
    {
        bucket = new();
    }

    public void AddUser(string user)
    {
        var newBucket = new LeakyBucket(5, 1);
        _ = new RequestProcessor(newBucket);
        bucket.Add(user, newBucket);
    }

    public bool CanSendRequest(string user)
    {
        if (!bucket.ContainsKey(user))
        {
            AddUser(user);
        }

        Console.WriteLine("---------------------");

        Console.Write(user);

        return bucket[user].CanAccess();
    }
}

