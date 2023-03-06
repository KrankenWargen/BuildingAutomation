using System.Diagnostics;
using WebApplicationGQL.Models;
using Neo4j.Driver;
using ServiceStack;
namespace GoldBeckLight.Repositories
{
    public class RoomRepository : IRoomRepository
    {

        private readonly IDriver driver;
        public RoomRepository(IDriver driver) {

            this.driver = driver;
        }

        public async Task<List<Room>> GetRooms()
        {

            using IAsyncSession session = driver.AsyncSession();
            List<Room> rooms = new List<Room>();
            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(n:Room)
RETURN collect(n.name) as rooms";
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {

                    record["rooms"].ConvertTo<List<string>>().ForEach(roomName =>
                    {
                        rooms.Add(new Room { Name = roomName });
                    });
                });


            });
            return rooms;
        }

        public async Task<List<Room>> GetRoomsByFloorName(string name)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Room> rooms = new List<Room>();
            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(n:Floor{name:'$name'})-[:CONTAIN]->(r:Room)
RETURN collect(r.name) as rooms";
                query = query.Replace("$name", name);
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {

                    record["rooms"].ConvertTo<List<string>>().ForEach(roomName =>
                    {
                        rooms.Add(new Room { Name = roomName });
                    });
                });


            });
            return rooms;
        }

    }
}
