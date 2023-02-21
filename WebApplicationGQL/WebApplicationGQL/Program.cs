using Neo4j.Driver;
using GoldBeckLight.Resolvers;
using GoldBeckLight.Types;
using GoldBeckLight.Repositories;
using HotChocolate.Data.Neo4J;
using System.Diagnostics;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080",
                                              "http://www.contoso.com");
                      });
});
IDriver driver = GraphDatabase.Driver(
              "neo4j+s://2725a56b.databases.neo4j.io",
              AuthTokens.Basic("neo4j", "rbAihX34NduwZaWL64EkKKwR_cYsf3UY5cMwJhqjidk"));

builder.Services.AddSingleton<IDriver>(driver)
    .AddScoped<IFloorRepository, FloorRepository>()
     .AddScoped<IRoomRepository, RoomRepository>()
     .AddScoped<IBuildingRepository, BuildingRepository>()
     .AddGraphQLServer()
      .AddDataLoader<RoomsByFloorDataLoader>()
      .AddDataLoader<FloorsByBuildingDataLoader>()
    .AddType<BuildingType>()
    .AddType<FloorResolver>()
   .AddType<FloorType>()
    .AddType<RoomResolver>()
      .AddQueryType(q => q.Name("Query"))
                        .AddType<WebApplicationGQL.GraphQL.Query>()
                        .AddFiltering()
                        .AddProjections();
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

app.MapGraphQL("/graphql");

app.Run();
