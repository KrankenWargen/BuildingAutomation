using GoldBeckLight.Repositories;
using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Floor")]
    public class FloorResolver
    {
            public Task<List<Floor>> GetFloorsAsync(
              [Parent] Building building,
              [Service] IFloorRepository floorRepository) => floorRepository.GetByName(building.Name);
        
    }
}
