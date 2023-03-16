using GoldBeckLight.Resolvers;
using System.Diagnostics;
using GoldBeckLight.Models;

namespace GoldBeckLight.Types
{

    public class BuildingType : ObjectType<Building>
    {
        protected override void Configure(IObjectTypeDescriptor<Building> descriptor)
        {
            
            descriptor.Field(_ => _.Name).IsProjected(true);
            descriptor.Field(_ => _.Floors).ResolveWith<FloorResolver>(_ => _.GetFloorsAsync(default, default));

        }
    }
}
