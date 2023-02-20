using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using Neo4j.Driver;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Floor")]
    public class FloorResolver
    {
       
   
        public async Task<List<Floor>> GetFloorsAsync(
              [Parent] Building building, FloorsByBuildingDataLoader floorsByBuildingDataLoader,
              CancellationToken cancellationToken)
        {
            // Load the rooms for the given floor using the data loader.

            var floorsByBuilding = await floorsByBuildingDataLoader.LoadAsync(building.Name);
            return floorsByBuilding ?? new List<Floor>();
            /*return _floorRepository.GetByName(building.Name);*/
        }





    }
}
