using GoldBeckLight.Repositories;
using System.Diagnostics;
using WebApplicationGQL.Models;

public class FloorsByBuildingDataLoader : BatchDataLoader<string, List<Floor>>
{
    private readonly IFloorRepository _repository;

    public FloorsByBuildingDataLoader(
        IFloorRepository repository,
        IBatchScheduler batchScheduler)
        : base(batchScheduler)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyDictionary<string, List<Floor>>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var floorsByBuilding = new Dictionary<string, List<Floor>>();

        foreach (var buildingName in keys)
        {
            var floors = await _repository.GetFloorByBuildingName(buildingName);
            floorsByBuilding[buildingName] = floors;
        }

        return floorsByBuilding;
    }
}