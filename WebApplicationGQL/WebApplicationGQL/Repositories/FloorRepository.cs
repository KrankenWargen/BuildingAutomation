using Neo4j.Driver;
using ServiceStack;
using System.Diagnostics;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public class FloorRepository : IFloorRepository
    {

        private readonly IDriver driver;
        public FloorRepository(IDriver driver) {

            this.driver = driver;
        }

        public async Task<List<Floor>> GetFloors()
        {

            IAsyncSession session = driver.AsyncSession();
            List<Floor> floors = new List<Floor>();
            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(n:Floor)
RETURN collect(n.name) as floors";
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {

                    record["floors"].ConvertTo<List<string>>().ForEach(floorName =>
                    {
                        floors.Add(new Floor { Name = floorName });
                    });
                });


            });
            return floors;
        }
        
        public async Task<List<Floor>> GetFloorByBuildingName(string name)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Floor> floors = new List<Floor>();
            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(n:Building{name:'$name'})-[:HAS]->(f:Floor)
RETURN collect(f.name) as floors";
                query = query.Replace("$name", name);
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {

                    record["floors"].ConvertTo<List<string>>().ForEach(floorName =>
                    {
                        floors.Add(new Floor { Name = floorName });
                    });
                });


            });
            return floors;
        }

    }
}
