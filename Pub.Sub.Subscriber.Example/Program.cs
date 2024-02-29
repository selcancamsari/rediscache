using StackExchange.Redis;

ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");
ISubscriber subscriber = connection.GetSubscriber();

//her türlü mesajı gösterir
//await subscriber.SubscribeAsync("mychannel", (channel, message) =>
//{
//    Console.WriteLine(message);
//});

//belirli bir patternde olan mesajlerı gösterir
await subscriber.SubscribeAsync("mychannel.*", (channel, message) =>
{
    Console.WriteLine(message);
});
Console.Read();