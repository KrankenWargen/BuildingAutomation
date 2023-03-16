using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using System;
using System.Diagnostics;
using GoldBeckLight.Models;
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
