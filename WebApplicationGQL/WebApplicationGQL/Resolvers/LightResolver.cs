using GoldBeckLight.Repositories;
using System.Diagnostics;
using GoldBeckLight.Models;
using GoldBeckLight.Repositories;

namespace GoldBeckLight.Resolvers
{
   
    public class LightResolver
    {

  
        public async Task<List<Light>> GetLightsAsync([Parent] Room room, [Service] ILightRepository lightRepository)
        {

            return await lightRepository.GetLightsByRoomName(room.Name);
        }




    }
}
