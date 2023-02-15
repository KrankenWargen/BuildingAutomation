using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;
using Neo4j.Driver;
using System;
using WebApplicationGQL.Data;
using WebApplicationGQL.Models;

namespace WebApplicationGQL.GraphQL
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {
        [GraphQLName("building")]
        [UseNeo4JDatabase("archeticture")]
        [UseProjection]

        public IExecutable<Building> GetBuildings(
          [ScopedService] IAsyncSession session) {

            return new Neo4JExecutable<Building>(session);
        }
           

        [GraphQLName("floor")]
        [UseNeo4JDatabase("archeticture")]
        [UseProjection]
        public IExecutable<Floor> GetFloors(
           [ScopedService]  IAsyncSession session) =>
           new Neo4JExecutable<Floor>(session);

    }
}
