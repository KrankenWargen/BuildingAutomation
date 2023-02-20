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

            var floorsByBuilding = await floorsByBuildingDataLoader.LoadAsync(building.Name,cancellationToken);
            return floorsByBuilding ?? new List<Floor>();
            /*return _floorRepository.GetByName(building.Name);*/
        }





    }
}
