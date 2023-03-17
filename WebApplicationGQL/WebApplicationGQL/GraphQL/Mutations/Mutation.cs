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

        /*   public readonly ConsumerConfig _config = new ConsumerConfig
           {

               BootstrapServers = "localhost:9092",
               GroupId = "1",
               AutoOffsetReset = AutoOffsetReset.Earliest
           };*/

        private readonly IConsumer<Ignore, string> consumerBuilder;
        private readonly IProducer<Null, string> producerBuilder;
        public Mutation(IConsumer<Ignore, string> consumerBuilder, IProducer<Null, string> producerBuilder)
        {
            Debug.WriteLine("22222");
            this.consumerBuilder = consumerBuilder;
            this.producerBuilder = producerBuilder;
        }
        public async Task<UpdateLightPayload> updateLight(UpdateLightInput input, [Service] ILightRepository lightRepository)
        {

            Debug.WriteLine("44444");
            var message = new Message<Null, string>
            {
                Value = $@"{{'Name':'{input.Name}','IsOn': {input.IsOn} }}"
            };


            var deliveryResult = producerBuilder.ProduceAsync("quickstart", message).GetAwaiter().GetResult();
            Debug.WriteLine(deliveryResult.Value);

            consumerBuilder.Subscribe("quickstart");
            var result = consumerBuilder.Consume(TimeSpan.FromSeconds(20));
            if (result == null)
            {
                throw new Exception("Timed out waiting for confirmation message.");
            }
            Debug.WriteLine(result.Message.Value.Replace("'", "\""));
            var lightConsumer = JsonSerializer.Deserialize<LightConsumer>(result.Message.Value.Replace("'", "\"").Replace("False", "false").Replace("True", "true"));
            Debug.WriteLine($"Light name2: {lightConsumer.Name}");

            Light light = new Light
            {
                Name = lightConsumer.Name,
                IsOn = lightConsumer.IsOn
            };
            Light updatedLight = await lightRepository.updateLight(light);
            return new UpdateLightPayload(updatedLight);




        }

    }
}
