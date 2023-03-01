using GoldBeckLight.Repositories;
using Neo4j.Driver;
using System.Diagnostics;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Light")]
    public class LightResolver
    {

        public readonly ILightRepository _lightRepository;
        public LightResolver(ILightRepository lightRepository) { 

            this._lightRepository = lightRepository; 
        }   
  
        public async Task<List<Light>> GetLightsAsync([Parent] Room room)
        {
          
            return await _lightRepository.GetLightsByFloorName(room.Name);
        }



    }
}
