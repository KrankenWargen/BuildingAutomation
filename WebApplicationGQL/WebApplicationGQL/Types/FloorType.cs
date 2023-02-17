using GoldBeckLight.Resolvers;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Types
{
    public class FloorType : ObjectType<Floor>
    {
        protected override void Configure(IObjectTypeDescriptor<Floor> descriptor)
        {
            descriptor.Field(_ => _.Name);
            descriptor.Field(_ => _.Rooms);

            // Creates the relationship between Product x Category
            descriptor.Field<RoomResolver>(_ => _.GetRoomsAsync(default, default));
        }
    }
}
