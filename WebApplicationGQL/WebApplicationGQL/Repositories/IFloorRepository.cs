using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public interface IFloorRepository
    {
        Task<List<Floor>> GetByName(string name);
    }
}
