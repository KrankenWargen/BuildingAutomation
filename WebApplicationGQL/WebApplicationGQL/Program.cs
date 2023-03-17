using GoldBeckLight.Resolvers;
using GoldBeckLight.Types;
using GoldBeckLight.Repositories;
using System.Diagnostics;
using GoldBeckLight.Models;
using GoldBeckLight.GraphQL.Queries;
using Neo4j.Driver;
using ServiceStack;
using ApacheKafkaConsumerDemo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GoldBeckLight.Repositories;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);
IDriver driver = GraphDatabase.Driver(
              "neo4j+s://2725a56b.databases.neo4j.io",
              AuthTokens.Basic("neo4j", "rbAihX34NduwZaWL64EkKKwR_cYsf3UY5cMwJhqjidk"));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



builder.Services
    .AddSingleton<IDriver>(driver)
    .AddSingleton<IConsumer<Ignore, string>>(new ConsumerBuilder<Ignore, string>(new ConsumerConfig
      {
        BootstrapServers = "localhost:9092",
        GroupId = "1",
        AutoOffsetReset = AutoOffsetReset.Earliest
    }).Build())
        .AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        }).Build())
    .AddHostedService<ApacheKafkaConsumerService>()
    .AddScoped<IBuildingRepository, BuildingRepository>()
    .AddScoped<IFloorRepository, FloorRepository>()
    .AddScoped<IRoomRepository, RoomRepository>()
    .AddScoped<ILightRepository, LightRepository>()
    .AddGraphQLServer()
    .AddType<BuildingType>()
    .AddType<FloorType>()
    .AddType<RoomType>()
    .AddQueryType(q => q.Name("Query"))
    .AddMutationType(q => q.Name("Mutation"))
                        .AddType<GoldBeckLight.GraphQL.Queries.Query>()
                        .AddType<GoldBeckLight.GraphQL.Mutations.Mutation>()
                        .AddFiltering()
                        .AddProjections();



var app = builder.Build();
app.UseCors("AllowAll");

app.MapGraphQL("/graphql");

app.Run();
