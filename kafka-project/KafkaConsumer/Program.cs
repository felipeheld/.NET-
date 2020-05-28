using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "demo-topic-consumer",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("demo-topic");

                var cancellationTokenSource = new CancellationTokenSource();

                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cancellationTokenSource.Cancel();
                };

                try {
                    while (true)
                    {
                        var consumed = consumer.Consume(cancellationTokenSource.Token);

                        Console.WriteLine(
                            $"Consumed message '{ consumed.Message.Value }' from topic { consumed.Topic }, partition { consumed.Partition }, offset { consumed.Offset }"
                        );
                    }
                } 
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"Topic consumption has been cancelled");                    
                }
                finally
                {
                    consumer.Close();
                }

            }

            
        }
    }
}
