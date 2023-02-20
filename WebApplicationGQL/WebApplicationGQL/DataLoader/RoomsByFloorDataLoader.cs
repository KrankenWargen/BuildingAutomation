using GoldBeckLight.Repositories;
using WebApplicationGQL.Models;

public class RoomsByFloorDataLoader : BatchDataLoader<string, List<Room>>
{
    private readonly IRoomRepository _repository;

    public RoomsByFloorDataLoader(
        IRoomRepository repository,
        IBatchScheduler batchScheduler)
        : base(batchScheduler)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyDictionary<string, List<Room>>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var roomsByFloor = new Dictionary<string, List<Room>>();

        foreach (var floorName in keys)
        {
            var rooms = await _repository.GetRoomsByFloorName(floorName);
            roomsByFloor[floorName] = rooms;
        }

        return roomsByFloor;
    }
}