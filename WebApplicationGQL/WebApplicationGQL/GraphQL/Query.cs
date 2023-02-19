using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;
using HotChocolate.Data.Neo4J.Language;
using Neo4j.Driver;
using ServiceStack;
using System;
using System.Diagnostics;
using WebApplicationGQL.Models;
using static HotChocolate.ErrorCodes;

namespace WebApplicationGQL.GraphQL
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {

        [GraphQLName("building")]
        [UseFiltering]
        public async Task<IQueryable<Building>> GetBuildings([Service] IDriver driver)
        {
            var executable = new Neo4JExecutable<Building>(driver.AsyncSession(o => o.WithDatabase("neo4j")));
            var buildings = await executable.ToListAsync(CancellationToken.None);
            return buildings.Cast<Building>().AsQueryable();
        }


       [GraphQLName("floor")]
        public async Task<IQueryable<Floor>> GetFloors(
           [Service] IDriver driver)
        {
            var executable = new Neo4JExecutable<Floor>(driver.AsyncSession(o => o.WithDatabase("neo4j")));
            var floors = await executable.ToListAsync(CancellationToken.None);
            return floors.Cast<Floor>().AsQueryable();
        }
          

    }
}
