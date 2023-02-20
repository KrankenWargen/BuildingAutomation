using Neo4j.Driver;
using ServiceStack;
using System.Diagnostics;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {

        private readonly IDriver driver;
        public BuildingRepository(IDriver driver) {

            this.driver = driver;
        }
        public async Task<List<Building>> GetBuildings()
        {

            IAsyncSession session = driver.AsyncSession();
            List<Building> buildings = new List<Building>();
            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(n:Building)
RETURN collect(n.name) as buildings";
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {

                    record["buildings"].ConvertTo<List<string>>().ForEach(buildingName =>
                    {
                        buildings.Add(new Building { Name = buildingName });
                    });
                });


            });
            return buildings;   
        }

    }
}
