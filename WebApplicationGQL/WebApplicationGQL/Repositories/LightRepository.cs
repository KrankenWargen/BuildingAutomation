using GoldBeckLight.Models;
using Neo4j.Driver;
using ServiceStack;
using System.Diagnostics;

namespace GoldBeckLight.Repositories
{
    public class LightRepository : ILightRepository
    {

        private readonly IDriver driver;
        public LightRepository(IDriver driver)
        {

            this.driver = driver;
        }

        public async Task<Light> updateLight(Light light)
        {
            IAsyncSession session = driver.AsyncSession();
            Light updatedLight = new Light();
            await session.ExecuteWriteAsync(async tx =>
            {
                var query = @"MATCH (l:Light {name: '$name'})
                SET l.ison = $ison
                RETURN { name : l.name,ison : l.ison } as Light";
                query = query.Replace("$name", light.Name);
                query = query.Replace("$ison", light.IsOn.ToString());
                var cursor = await tx.RunAsync(query);
                var record = await cursor.ToListAsync();


                Dictionary<string, string> lightResponse = record.FirstOrDefault().Values["Light"].ConvertTo<Dictionary<string, string>>();
                updatedLight = new Light
                {
                    Name = lightResponse["name"],
                    IsOn = bool.Parse(lightResponse["ison"])
                };

            });
            return updatedLight;
        }
        public async Task<List<Light>> GetLightsByRoomName(string roomName)
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

                    record["lights"].ConvertTo<List<Dictionary<string, string>>>().ForEach(light =>
                    {
                        lights.Add(new Light
                        {
                            Name = light["name"].As<string>(),
                            IsOn = light["ison"].As<bool>()

                        });
                    });
                });


            });

            return lights;
        }


    }
}
