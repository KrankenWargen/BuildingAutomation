using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using Neo4j.Driver;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    
    public class FloorResolver
    {


        public async Task<List<Floor>> GetFloorsAsync(
              [Parent] Building building, [Service] IFloorRepository floorRepository)
        {
           

            /*      var floorsByBuilding = await floorsByBuildingDataLoader.LoadAsync(building.Name,cancellationToken);
                  return floorsByBuilding ?? new List<Floor>();*/
            return await floorRepository.GetFloorByBuildingName(building.Name);
        }





    }
}
