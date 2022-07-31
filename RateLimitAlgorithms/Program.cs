using RateLimitAlgorithms;

//TokenBucket tokenBucket = new(5, 10);

//Console.WriteLine(tokenBucket.GetAvailableCapacity());

//for (int i = 0; i < 10; i++)
//{
//    if (i == 7)
//    {
//        Thread.Sleep(1000);
//    }
//    Console.WriteLine($"{DateTime.Now}-{tokenBucket.Consume()}");
//}


Console.WriteLine("---------------------------------");

BucketCreator creator = new();

creator.AddUser("abc");

creator.AddUser("xyz");

for (int i = 0; i < 10; i++)
{
    if (i == 7) Thread.Sleep(1000);
    creator.CanSendRequest("xyz");
}

for (int i = 0; i < 10; i++)
{
    creator.CanSendRequest("abc");
}

Console.WriteLine("---------------------------------");