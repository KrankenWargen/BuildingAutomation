using GoldBeckLight.Resolvers;
using WebApplicationGQL.Models;

namespace GoldBeckLight.Types
{
    public class BuildingType : ObjectType<Building>
    {
        protected override void Configure(IObjectTypeDescriptor<Building> descriptor)
        {
            descriptor.Field(_ => _.Name);
            descriptor.Field(_ => _.Floors);

            // Creates the relationship between Product x Category
            descriptor.Field<FloorResolver>(_ => _.GetFloorsAsync(default, default));
        }
    }
}
