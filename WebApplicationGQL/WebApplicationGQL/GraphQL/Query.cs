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
        [UseNeo4JDatabase("neo4j")]
        [UseProjection]
       
        public Neo4JExecutable<Building> GetBuildings(
          [ScopedService] IAsyncSession session) {
            return new(session);
        }
           

        [GraphQLName("floor")]
        [UseNeo4JDatabase("neo4j")]
        [UseProjection]
        public Neo4JExecutable<Floor> GetFloors(
           [ScopedService]  IAsyncSession session)
        {
            return new(session);
        }
          

    }
}
