using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using GoldBeckLight.Models.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;
using ServiceStack.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApacheKafkaConsumerDemo
{
    public class ApacheKafkaConsumerService : IHostedService
    {
        private readonly string topic = "quickstart";
        private readonly string groupId = "1";
        private readonly string bootstrapServers = "localhost:9092";

        private readonly IConsumer<Ignore, string> consumerBuilder;

        public ApacheKafkaConsumerService(IConsumer<Ignore, string> consumerBuilder)
        {
            Debug.WriteLine("000000");
            this.consumerBuilder = consumerBuilder;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("11111111111");

           

            consumerBuilder.Subscribe(topic);
            Task.Run(() =>
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var consumer = consumerBuilder.Consume(cancellationToken);
                        var lightConsumer = JsonSerializer.Deserialize<LightConsumer>(consumer.Message.Value.Replace("'", "\"").Replace("False", "false").Replace("True", "true"));
                        Debug.WriteLine($"Light name: {lightConsumer.Name} IsOn: {lightConsumer.IsOn}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            consumerBuilder?.Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            consumerBuilder?.Dispose();
        }
    }
}