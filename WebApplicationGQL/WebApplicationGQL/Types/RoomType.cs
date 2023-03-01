using GoldBeckLight.Resolvers;
using HotChocolate.Types;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Types
{
    public class RoomType : ObjectType<Room>
    {
        protected override void Configure(IObjectTypeDescriptor<Room> descriptor)
        {
            descriptor.Field(_ => _.Name).IsProjected(true);

        }
    }
}
