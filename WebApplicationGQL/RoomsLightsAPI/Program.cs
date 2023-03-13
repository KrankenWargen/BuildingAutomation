using Neo4j.Driver;
using GoldBeckLight.Repositories;
using HotChocolate.Data.Neo4J;
using System.Diagnostics;
using WebApplicationGQL.Models;
using WebApplicationGQL.GraphQL;

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


builder.Services.AddSingleton<IDriver>(driver)
    . AddScoped<ILightRepository, LightRepository>()
     .AddGraphQLServer()
      .AddQueryType(q => q.Name("Query"))
                        .AddType<WebApplicationGQL.GraphQL.Query>()
                        .AddFiltering()
                        .AddProjections()
                         .PublishSchemaDefinition(c => c
                    .SetName("rooms")
                      .IgnoreRootTypes()
                      .AddTypeExtensionsFromFile("./Stitching.graphql")); ;
var app = builder.Build();
app.UseCors("AllowAll");

app.MapGraphQL("/graphql");

app.Run();
