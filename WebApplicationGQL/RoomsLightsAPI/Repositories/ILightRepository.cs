using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public interface ILightRepository
    {
        Task<List<Light>> GetLightsByFloorName(string roomName);
    }
}
