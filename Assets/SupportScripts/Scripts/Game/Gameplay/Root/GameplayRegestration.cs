using DI;

public static class GameplayRegestration
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        var gameState = gameStateProvider.GameState;

        var cmd = new CommandProcessor(gameStateProvider);
        cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
        container.RegisterInstance<ICommandProcessor>(cmd);



        container.RegisterFactory(_ => new BuildingService(gameState.Buildings, cmd)).AsSingle();
    }
}



