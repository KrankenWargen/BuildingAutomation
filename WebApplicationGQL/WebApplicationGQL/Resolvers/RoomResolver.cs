using GoldBeckLight.Repositories;
using Neo4j.Driver;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
    [ExtendObjectType(Name = "Room")]
    public class RoomResolver
    {

        private readonly RoomsByFloorDataLoader _roomsByFloorDataLoader;

        public RoomResolver(RoomsByFloorDataLoader roomsByFloorDataLoader) { 
                this._roomsByFloorDataLoader = roomsByFloorDataLoader;
        }
        public async Task<List<Room>> GetRoomsAsync([Parent] Floor floor, CancellationToken cancellationToken)
        {
            // Load the rooms for the given floor using the data loader.
            var roomsByFloor = await _roomsByFloorDataLoader.LoadAsync(floor.Name, cancellationToken);
            return roomsByFloor ?? new List<Room>();
        }


        public async Task<int> GetNumberOfRooms([Parent] Floor floor, CancellationToken cancellationToken)
        {
            // Load the rooms for the given floor using the data loader.
            var roomsByFloor = await _roomsByFloorDataLoader.LoadAsync(floor.Name, cancellationToken);
            var numberOfRooms = roomsByFloor?.Count ?? 0;
            return numberOfRooms;
        }

    }
}
