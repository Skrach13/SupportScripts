using DI;

public static class GameplayRegestration
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        container.RegisterFactory(
            c => new SomeGameplayService(
                c.Resolve<IGameStateProvider>().GameState,
                    c.Resolve<SomeCommonService>())).AsSingle();
    }
}



