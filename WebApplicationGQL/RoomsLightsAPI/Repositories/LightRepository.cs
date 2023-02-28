using Neo4j.Driver;
using ServiceStack;
using System.Diagnostics;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Repositories
{
    public class LightRepository : ILightRepository
    {

        private readonly IDriver driver;
        public LightRepository(IDriver driver) {

            this.driver = driver;
        }
        public async Task<List<Light>> GetLightsByFloorName(string roomName)
        {

            IAsyncSession session = driver.AsyncSession();
            List<Light> lights = new List<Light>();
            await session.ExecuteReadAsync(async tx =>
            {
                var query = @"MATCH(r:Room{name:'$name'})-[:HOLD]->(l:Light)
RETURN collect({ name : l.name,ison : l.ison }) as lights";
                query = query.Replace("$name", roomName);
                var cursor = await tx.RunAsync(query);
                var records = await cursor.ToListAsync();

                records.ForEach(record =>
                {

                    record["lights"].ConvertTo<List<Dictionary<string,string>>>().ForEach(light =>
                    {
                        lights.Add(new Light
                        {
                            Name = light["name"].As<string>(),
                            IsOn = light["ison"].As<bool>()

                        }); 
                    });
                });


            });
            Debug.WriteLine(lights);
            return lights;   
        }

    
    }
}
