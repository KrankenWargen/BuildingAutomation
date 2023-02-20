using GoldBeckLight.Repositories;
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
        public Neo4JExecutable<Building> GetBuildings([Service] IDriver driver)
        {
            var executable = new Neo4JExecutable<Building>(driver.AsyncSession(o => o.WithDatabase("neo4j")));

            return executable;
        }



        [GraphQLName("floor")]
        public Neo4JExecutable<Floor> GetFloors(
           [Service] IDriver driver)
        {
            var executable = new Neo4JExecutable<Floor>(driver.AsyncSession(o => o.WithDatabase("neo4j")));

            return executable;
        }
          

    }
}
