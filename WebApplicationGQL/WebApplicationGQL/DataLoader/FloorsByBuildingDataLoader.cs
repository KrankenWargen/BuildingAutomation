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
            var rooms = await _repository.GetByName(buildingName);
            floorsByBuilding[buildingName] = rooms;
        }

        return floorsByBuilding;
    }
}