using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using System;
using System.Diagnostics;
using WebApplicationGQL.Models;
using static HotChocolate.ErrorCodes;

namespace GoldBeckLight.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class QueryFloor
    {





        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Floor>> GetFloorByBuildingName(string buildingName, [Service] IFloorRepository floorRepository)
        {
            return await floorRepository.GetFloorByBuildingName(buildingName);
        }


    }
}
