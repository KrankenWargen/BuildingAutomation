using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public interface IBuildingRepository
    {
        Task<List<Building>> GetBuildings();
    }
}
