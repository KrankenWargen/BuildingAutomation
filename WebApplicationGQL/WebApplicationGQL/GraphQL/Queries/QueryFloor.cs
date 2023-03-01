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
    public class QueryFloor
    {





        [UseProjection]
        [UseFiltering]
        public async Task<IEnumerable<Floor>> GetFloorByBuildingName(string buildingName,[Service] IFloorRepository floorRepository)
        {
            return await floorRepository.GetFloorByBuildingName(buildingName);
        }


    }
}
