using GoldBeckLight.Repositories;
using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Room")]
    public class RoomResolver
    {

  
        public async Task<List<Room>> GetRoomsAsync([Parent] Floor floor, RoomsByFloorDataLoader roomsByFloorDataLoader, CancellationToken cancellationToken)
        {
           
            var roomsByFloor = await roomsByFloorDataLoader.LoadAsync(floor.Name, cancellationToken);
            return roomsByFloor ?? new List<Room>();
            /*       return _roomRepository.GetByName(floor.Name); */
        }


        public async Task<int> GetNumberOfRooms([Parent] Floor floor, RoomsByFloorDataLoader roomsByFloorDataLoader,CancellationToken cancellationToken)
        {
          
            var roomsByFloor = await roomsByFloorDataLoader.LoadAsync(floor.Name,cancellationToken);
            var numberOfRooms = roomsByFloor?.Count ?? 0;
            return numberOfRooms;
        }

    }
}
