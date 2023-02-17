using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetByName(string name);
    }
}
