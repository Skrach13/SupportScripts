using DI;

public static class MainMenuViewModelRegistration
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new UIMainMenuRootViewModel()).AsSingle();
    }
}