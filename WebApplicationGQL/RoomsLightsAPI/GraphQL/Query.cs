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

namespace WebApplicationGQL.GraphQL
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {

        private readonly IRoomRepository _roomRepository;
        public Query( IRoomRepository roomRepository) {

            _roomRepository = roomRepository;

        }  
       


        [GraphQLName("room")]
        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _roomRepository.GetRooms();
        }
    }
}
