using Neo4j.Driver;
using GoldBeckLight.Models;

namespace GoldBeckLight.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetRooms();
        Task<List<Room>> GetRoomsByFloorName(string name);
    }
}
