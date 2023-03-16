using GoldBeckLight.Repositories;
using System.Diagnostics;
using GoldBeckLight.Models;

namespace GoldBeckLight.Resolvers
{
   
    public class RoomResolver
    {

  
        public async Task<List<Room>> GetRoomsAsync([Parent] Floor floor, [Service] IRoomRepository roomRepository)
        {

            return await roomRepository.GetRoomsByFloorName(floor.Name);
        }




    }
}
