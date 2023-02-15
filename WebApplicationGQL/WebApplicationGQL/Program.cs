using Microsoft.EntityFrameworkCore;
using WebApplicationGQL.Data;
using WebApplicationGQL.GraphQL;
using GraphQL.Server.Ui.Voyager;
using WebApplicationGQL.Types;
using Neo4j.Driver;

var builder = WebApplication.CreateBuilder(args);
IDriver driver = GraphDatabase.Driver(
              "bolt://localhost:7687",
              AuthTokens.Basic("neo4j", "goldbecklight"));

builder.Services.AddSingleton(driver)
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<WebApplicationGQL.GraphQL.Query>()
    .AddProjections();
var app = builder.Build();


app.MapGraphQL();

app.Run();
