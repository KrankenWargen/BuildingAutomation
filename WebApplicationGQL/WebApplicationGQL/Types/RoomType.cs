using GoldBeckLight.Resolvers;
using HotChocolate.Types;
using GoldBeckLight.Models;

namespace GoldBeckLight.Types
{
    public class RoomType : ObjectType<Room>
    {
        protected override void Configure(IObjectTypeDescriptor<Room> descriptor)
        {
            descriptor.Field(_ => _.Name).IsProjected(true);
            descriptor.Field(_ => _.Lights)
        .ResolveWith<LightResolver>(_ => _.GetLightsAsync(default, default));

        }
    }
}
