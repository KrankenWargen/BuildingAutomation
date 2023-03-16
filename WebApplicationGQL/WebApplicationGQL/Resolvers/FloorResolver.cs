using GoldBeckLight.Repositories;
using GoldBeckLight.Types;
using Neo4j.Driver;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using GoldBeckLight.Models;

namespace GoldBeckLight.Resolvers
{
    
    public class FloorResolver
    {


        public  Task<List<Floor>> GetFloorsAsync(
              [Parent] Building building, [Service] IFloorRepository floorRepository)
        {
            
            return  floorRepository.GetFloorByBuildingName(building.Name);
        }





    }
}
