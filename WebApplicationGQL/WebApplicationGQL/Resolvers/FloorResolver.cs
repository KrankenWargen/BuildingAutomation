using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using Neo4j.Driver;
using System.Drawing;
using System.Threading;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Floor")]
    public class FloorResolver
    {
        private readonly FloorsByBuildingDataLoader _floorsByBuildingDataLoader;

        public FloorResolver(FloorsByBuildingDataLoader floorsByBuildingDataLoader)
        {
            this._floorsByBuildingDataLoader = floorsByBuildingDataLoader;
        }
        public async Task<List<Floor>> GetFloorsAsync(
              [Parent] Building building,
              CancellationToken cancellationToken)
        {
            // Load the rooms for the given floor using the data loader.
            var floorsByBuilding = await _floorsByBuildingDataLoader.LoadAsync(building.Name, cancellationToken);
            return floorsByBuilding ?? new List<Floor>();
        }





    }
}
