
using GoldBeckLight.Repositories;
using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using GoldBeckLight.GraphQL.Queries;
using Neo4j.Driver;
using ServiceStack;
using ApacheKafkaConsumerDemo;
using GoldBeckLight.Repositories;
using GoldBeckLight.Types;

var builder = WebApplication.CreateBuilder(args);
IDriver driver = GraphDatabase.Driver(
              "neo4j://localhost:7687",
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
    /*.AddHostedService<ApacheKafkaConsumerService>()*/
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

