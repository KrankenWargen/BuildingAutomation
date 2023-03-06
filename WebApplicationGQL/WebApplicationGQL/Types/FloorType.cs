using GoldBeckLight.Resolvers;
using HotChocolate.Types;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Types
{
    public class FloorType : ObjectType<Floor>
    {
        protected override void Configure(IObjectTypeDescriptor<Floor> descriptor)
        {
            descriptor.Field(_ => _.Name).IsProjected(true); 
            descriptor.Field(_ => _.Rooms)
                .ResolveWith<RoomResolver>(_ => _.GetRoomsAsync(default,default,default));

        }
    }
}
