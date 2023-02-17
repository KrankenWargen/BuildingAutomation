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
       
        public async Task<List<Building>> GetBuildings(
          [ScopedService] IAsyncSession session) {
            List<Building> allBuildings = new List<Building>();

             await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(n:Building)-[:HAS]->(f:Floor)
WITH n, collect(f.name) as floors
RETURN n{.*, floors: floors} as buildingType";
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();
                
                records.ForEach(record =>
                {
                    Dictionary<string, object> buildingType = record["buildingType"].ToObjectDictionary();
                    List<Floor> floors= new List<Floor>();
                    buildingType["floors"].ConvertTo<List<string>>().ForEach(floorName =>
                    {
                        floors.Add(new Floor { Name= floorName });
                    });
                    var building = new Building
                    {
                        Name = buildingType["name"].ToString(),
                        Floors = floors

                    };
                    allBuildings.Add(building); 
                });
            

            });
            return allBuildings;
        }
           

        [GraphQLName("floor")]
        [UseNeo4JDatabase("neo4j")]
        [UseProjection]
        public async Task<List<Floor>> GetFloors(
           [ScopedService]  IAsyncSession session)
        {
            List<Floor> allFloorss = new List<Floor>();

            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(f:Floor)-[:CONTAIN]->(r:Room)
WITH f, collect(r.name) as rooms
RETURN f{.*, rooms: rooms} as floorType";
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {
                    Dictionary<string, object> floorType = record["floorType"].ToObjectDictionary();
                    List<Room> rooms = new List<Room>();
                    floorType["rooms"].ConvertTo<List<string>>().ForEach(roomName =>
                    {
                        rooms.Add(new Room { Name = roomName });
                    });
                    var floor = new Floor
                    {
                        Name = floorType["name"].ToString(),
                        Rooms = rooms

                    };
                    allFloorss.Add(floor);
                });


            });
            return allFloorss;
        }
          

    }
}
