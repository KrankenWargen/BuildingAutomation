using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;
using HotChocolate.Data.Neo4J.Language;
using Neo4j.Driver;
using ServiceStack;
using System;
using System.Diagnostics;
using WebApplicationGQL.Models;
using static HotChocolate.ErrorCodes;

namespace GoldBeckLight.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class QueryRoom
    {


        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Room>> GetRoomsByFloorName(string floorName,[Service] IRoomRepository roomRepository)
        {
            return await roomRepository.GetRoomsByFloorName(floorName);
        }


    }
}
