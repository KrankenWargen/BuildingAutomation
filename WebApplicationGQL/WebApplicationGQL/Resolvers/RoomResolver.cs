using GoldBeckLight.Repositories;
using System.Diagnostics;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Resolvers
{
   
    public class RoomResolver
    {

  
        public async Task<List<Room>> GetRoomsAsync([Parent] Floor floor, [Service] RoomsByFloorDataLoader roomsByFloorDataLoader,CancellationToken cancellationToken)
        {

            var roomsByFloor = await roomsByFloorDataLoader.LoadAsync(floor.Name, cancellationToken);
            return roomsByFloor ?? new List<Room>();
           /* return await roomRepository.GetRoomsByFloorName(floor.Name);*/
        }


   

    }
}
