using Neo4j.Driver;
using GoldBeckLight.Resolvers;
using GoldBeckLight.Types;
using GoldBeckLight.Repositories;
using HotChocolate.Data.Neo4J;
using System.Diagnostics;
using WebApplicationGQL.Models;
using GoldBeckLight.GraphQL.Queries;

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
    .AddType<RoomType>()
      .AddQueryType(q => q.Name("Query"))
                        .AddType<GoldBeckLight.GraphQL.Queries.Query>()
                        .AddType<QueryFloor>()
                        .AddType<QueryRoom>()
                        .AddFiltering()
                        .AddProjections()
                        .PublishSchemaDefinition(c => c
                    .SetName("buildings")
                      .IgnoreRootTypes().AddTypeExtensionsFromFile("./Stitching.graphql"));
var app = builder.Build();
app.UseCors("AllowAll");

app.MapGraphQL("/graphql");

app.Run();
