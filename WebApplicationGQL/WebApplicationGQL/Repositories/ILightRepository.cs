using GoldBeckLight.Models;
using Neo4j.Driver;

namespace GoldBeckLight.Repositories
{
    public interface ILightRepository
    {
        Task<Light> updateLight(Light light);
        Task<List<Light>> GetLightsByRoomName(string roomName);
    }
}
