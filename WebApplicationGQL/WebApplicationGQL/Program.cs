using Microsoft.EntityFrameworkCore;
using WebApplicationGQL.GraphQL;
using GraphQL.Server.Ui.Voyager;
using Neo4j.Driver;
using HotChocolate.Data.Neo4J;

var builder = WebApplication.CreateBuilder(args);
IDriver driver = GraphDatabase.Driver(
              "neo4j+s://2725a56b.databases.neo4j.io",
              AuthTokens.Basic("neo4j", "rbAihX34NduwZaWL64EkKKwR_cYsf3UY5cMwJhqjidk"));

builder.Services.AddSingleton(driver)
    .AddGraphQLServer()
     .AddQueryType(q => q.Name("Query"))
                        .AddType<WebApplicationGQL.GraphQL.Query>()
    .AddProjections();
var app = builder.Build();


app.MapGraphQL();

app.Run();
