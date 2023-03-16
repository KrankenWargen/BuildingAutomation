using Neo4j.Driver;
using GoldBeckLight.Models;

namespace GoldBeckLight.Repositories
{
    public interface IBuildingRepository
    {
        Task<List<Building>> GetBuildings();
    }
}
