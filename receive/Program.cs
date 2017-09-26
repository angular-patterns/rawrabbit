using RawRabbit.Context;
using RawRabbit.vNext;
using Shared;
using System;


namespace receive
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receive Program");
            Console.WriteLine("----------------");
            Console.WriteLine("RabbitMQ management website: http://localhost:15672/");

            var client = BusClientFactory.CreateDefault<AdvancedMessageContext>();
            client.SubscribeAsync<BasicMessage>(async (msg, context) =>
            {
                if (msg.Text == "throw")
                {
                    throw new Exception("An Error Occurred");
                }
                if (msg.Text.StartsWith("wait "))
                {
                    int result;
                    if (Int32.TryParse(msg.Text.Substring(5), out result))
                    {
                        System.Threading.Thread.Sleep(result * 1000);
                    }

                }

                if (context.RetryInfo.NumberOfRetries > 3)
                {
                    throw new Exception($"Unable to handle message '{context.GlobalRequestId}'.");
                }

                if (msg.Text.StartsWith("retry"))
                {
                    context.RetryLater(TimeSpan.FromSeconds(5.0));
                    Console.WriteLine("Retrying attempt #" + context.RetryInfo.NumberOfRetries);
                    return;
                }
                Console.WriteLine($"Recieved: {msg.Text}.");
            });

      
            
           
        }
    }
}
