using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var Buildings = "buildings";
var Rooms = "rooms";

builder.Services.AddHttpClient(Buildings, c => c.BaseAddress = new Uri("https://localhost:7271/graphql/"));
builder.Services.AddHttpClient(Rooms, c => c.BaseAddress = new Uri("https://localhost:7092/graphql/"));

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
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddRemoteSchema(Buildings)
    .AddRemoteSchema(Rooms)
.AddInstrumentation();

builder.Logging.AddOpenTelemetry(

    b =>
{
    b.IncludeFormattedMessage = true;
    b.IncludeScopes = true;
    b.ParseStateValues = true;
    b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Demo"));
});

builder.Services.AddOpenTelemetryTracing(

    b =>
{
   b.AddHttpClientInstrumentation();
   b.AddAspNetCoreInstrumentation();
   b.AddHotChocolateInstrumentation();
   b.AddJaegerExporter();
});



var app = builder.Build();
app.UseCors("AllowAll");
app.MapGraphQL("/graphql");

app.Run();
