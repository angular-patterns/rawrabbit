using RawRabbit.Context;
using RawRabbit.vNext;
using Shared;
using System;


namespace send
{



    class Program
    {
         static void Main(string[] args)
        {
            Console.WriteLine("Send Program");
            Console.WriteLine("----------------");
            Console.WriteLine("RabbitMQ management website: http://localhost:15672/");
            var client = BusClientFactory.CreateDefault<AdvancedMessageContext>();
            while (true)
            {
                var line = Console.ReadLine();
                var response = client.PublishAsync(new BasicMessage { Text = line });
            }
            
        }
    }
}
