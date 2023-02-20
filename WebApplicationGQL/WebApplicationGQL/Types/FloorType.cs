using GoldBeckLight.Resolvers;
using HotChocolate.Types;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Types
{
    public class FloorType : ObjectType<Floor>
    {
        protected override void Configure(IObjectTypeDescriptor<Floor> descriptor)
        {
            descriptor.Field(_ => _.Name);
            descriptor.Field(_ => _.Rooms)
                .ResolveWith<RoomResolver>(_ => _.GetRoomsAsync(default,default,default));
            descriptor.Field(_ => _.NumberOfRooms).ResolveWith<RoomResolver>(_ => _.GetNumberOfRooms(default, default,default));

        }
    }
}
