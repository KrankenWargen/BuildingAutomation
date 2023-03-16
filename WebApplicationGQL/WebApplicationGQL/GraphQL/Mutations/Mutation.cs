using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using Neo4j.Driver;
using System;
using System.Diagnostics;
using GoldBeckLight.Models;
using static HotChocolate.ErrorCodes;
using GoldBeckLight.Repositories;
using GoldBeckLight.GraphQL.Mutations.Input;
using GoldBeckLight.GraphQL.Mutations.Payload;
using Confluent.Kafka;
using GoldBeckLight.Models.Kafka;
using JsonSerializer = System.Text.Json.JsonSerializer;
namespace GoldBeckLight.GraphQL.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class Mutation
    {

        public readonly ConsumerConfig _config = new ConsumerConfig
        {

            BootstrapServers = "localhost:9092",
            GroupId = "1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };


        public async Task<UpdateLightPayload> updateLight(UpdateLightInput input, [Service] ILightRepository lightRepository)
        {
           
           /* var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe("quickstart");
            var result = consumer.Consume(TimeSpan.FromSeconds(60));
            if (result == null)
            {
                throw new Exception("Timed out waiting for confirmation message.");
            }
         
            var lightConsumer = JsonSerializer.Deserialize<LightConsumer>(result.Message.Value);
            Debug.WriteLine($"Light name: {lightConsumer.Name}");*/

            Light light = new Light
            {
                Name = input.Name,
                IsOn = input.IsOn
            };
            Light updatedLight = await lightRepository.updateLight(light);
            return new UpdateLightPayload(updatedLight);
        }

    }
}
