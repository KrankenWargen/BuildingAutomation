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
        private readonly IBuildingRepository _buildingRepository;
        private readonly IFloorRepository _floorRepository;
        private readonly IRoomRepository _roomRepository;
        public Query(IBuildingRepository buildingRepository, IFloorRepository floorRepository, IRoomRepository roomRepository) {

            _buildingRepository = buildingRepository;
            _floorRepository = floorRepository;
            _roomRepository = roomRepository;
        }  
       
   

        [GraphQLName("building")]
        [UsePaging(typeof(BuildingType))]
        [UseProjection]
        [UseFiltering]
       
        public async Task<IEnumerable<Building>> GetBuildings()
        {
            return await _buildingRepository.GetBuildings();
        }



        [GraphQLName("floor")]
        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Floor>> GetFloors()
        {
            return await _floorRepository.GetFloors();
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
