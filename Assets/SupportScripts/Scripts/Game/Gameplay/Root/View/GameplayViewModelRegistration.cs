using DI;

public static class GameplayViewModelRegistration 
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
        container.RegisterFactory(c => new WorldGameplayRootViewModel(c.Resolve<BuildingService>())).AsSingle();
    }
}
