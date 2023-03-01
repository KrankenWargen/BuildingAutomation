var builder = WebApplication.CreateBuilder(args);

var Buildings = "buildings";
var Rooms = "rooms";

builder.Services.AddHttpClient(Buildings, c => c.BaseAddress = new Uri("https://localhost:7271/graphql/"));


builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddRemoteSchema(Buildings);



var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();
