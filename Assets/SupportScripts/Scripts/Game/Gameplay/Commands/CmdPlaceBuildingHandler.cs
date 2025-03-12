
public class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuilding>
{
    private readonly GameStateProxy _gameStateProxy;
    public CmdPlaceBuildingHandler(GameStateProxy gameStateProxy) 
    {
        _gameStateProxy = gameStateProxy;
    }
    public bool Handle(CmdPlaceBuilding command)
    {
        var entityId = _gameStateProxy.GatEntityId();
        var newBuildingEmtity = new BuildingEntity
        {
            Id = entityId,
            Position = command.Position,
            TypeId = command.BuildingTypeId
        };

        var mewBuildingEntityProxy = new BuildingEntityProxy(newBuildingEmtity);

        _gameStateProxy.Buildings.Add(mewBuildingEntityProxy);

        return true;
    }
}