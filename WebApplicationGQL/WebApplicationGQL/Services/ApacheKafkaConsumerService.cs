using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using GoldBeckLight.Models.Kafka;

namespace ApacheKafkaConsumerDemo
{
    public class ApacheKafkaConsumerService : IHostedService
    {
        private readonly string topic = "quickstart";
        private readonly string groupId = "1";
        private readonly string bootstrapServers = "localhost:9092";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("11111111111");

            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                      while (true)
                        {
                            var consumer = consumerBuilder.Consume
                               (cancelToken.Token);
                          try
                    
                        {
                    
                            var lightConsumer = JsonSerializer.Deserialize
                                <LightConsumer>
                                    (consumer.Message.Value);
                            Debug.WriteLine($"Light name: {lightConsumer.Name}");
                        }
                        catch (Exception ex)
                   
                        {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                   
                        }
              
                    }
                 
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}