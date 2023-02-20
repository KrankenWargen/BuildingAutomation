using Neo4j.Driver;
using GoldBeckLight.Resolvers;
using GoldBeckLight.Types;
using GoldBeckLight.Repositories;
using HotChocolate.Data.Neo4J;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
IDriver driver = GraphDatabase.Driver(
              "enterServerUrl",
              AuthTokens.Basic("enterName", "enterPassword"));

builder.Services.AddSingleton<IDriver>(driver)
    .AddScoped<IFloorRepository, FloorRepository>()
     .AddScoped<IRoomRepository, RoomRepository>()
     .AddGraphQLServer()
      .AddDataLoader<RoomsByFloorDataLoader>()
      .AddDataLoader<FloorsByBuildingDataLoader>()
    .AddType<BuildingType>()
    .AddType<FloorResolver>()
   .AddType<FloorType>()
    .AddType<RoomResolver>()
   
      .AddQueryType(q => q.Name("Query"))
                        .AddType<WebApplicationGQL.GraphQL.Query>()
                        .AddFiltering();
var app = builder.Build();


app.MapGraphQL("/graphql");

app.Run();
