using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using Neo4j.Driver;
using System;
using System.Diagnostics;
using WebApplicationGQL.Models;
using static HotChocolate.ErrorCodes;

namespace GoldBeckLight.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {



        /*    [UsePaging(typeof(BuildingType))]*/
        [UseProjection]
        [UseFiltering]

        public async Task<IEnumerable<Building>> GetBuildings([Service] IBuildingRepository buildingRepository)
        {
            return await buildingRepository.GetBuildings();
        }




        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Floor>> GetFloors([Service] IFloorRepository floorRepository)
        {
            return await floorRepository.GetFloors();
        }


        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Room>> GetRooms([Service] IRoomRepository roomRepository)
        {
            return await roomRepository.GetRooms();
        }
    }
}
