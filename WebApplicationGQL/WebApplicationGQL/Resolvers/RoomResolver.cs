using GoldBeckLight.Repositories;
using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Room")]
    public class RoomResolver
    {
            public Task<List<Room>> GetRoomsAsync(
              [Parent] Floor floor,
              [Service] IRoomRepository roomRepository) => roomRepository.GetByName(floor.Name);
        
    }
}
