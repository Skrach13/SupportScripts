using R3;

public interface IGameStateProvider
{
    public GameStateProxy GameState { get;}
    public GameSettingsStateProxy SettingsState { get;}

    public Observable<GameStateProxy> LoadGameState();
    public Observable<GameSettingsStateProxy> LoadSettingsState();
    public Observable<bool> SaveGameState();
    public Observable<bool> SaveSettingsState();
    public Observable<bool> ResetGameState();
    public Observable<GameSettingsStateProxy> ResetSettingsState();
}