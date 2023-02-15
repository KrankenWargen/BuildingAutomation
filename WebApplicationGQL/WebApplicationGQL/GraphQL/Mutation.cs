using WebApplicationGQL.Data;
using WebApplicationGQL.Models;
using WebApplicationGQL.Types;

namespace WebApplicationGQL.GraphQL
{
    public class Mutation
    {
   
        public async Task<AddPlatformPayload> AddPlatformAsync( AppDbContext context, string name)
        {
            var platform = new Building
            {

                Name = name
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            return new AddPlatformPayload(platform);
        }
    }
}
