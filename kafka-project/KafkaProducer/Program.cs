using System;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig 
            {
                BootstrapServers = "localhost:9092"                    
            };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                    ? $"Delivered message to { r.TopicPartitionOffset }"
                    : $"Delivery error: { r.Error.Reason }");

            
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                string stringValue = "";
                
                while (true)
                {
                    await producer.ProduceAsync("demo-topic", new Message<Null, string> { Value = stringValue += "." });
                }                   
            }
        }
    }
}
