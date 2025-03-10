using DI;

public static class MainMenuRegestration
{
    public static void Register(DIContainer container, MainMenuEnterParams mainmenuEnterParams)
    {
        container.RegisterFactory(c => new SomeMeinMenyService(c.Resolve<SomeCommonService>()));
    }
}



