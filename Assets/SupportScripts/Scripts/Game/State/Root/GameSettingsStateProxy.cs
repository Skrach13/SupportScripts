using R3;

public class GameSettingsStateProxy
{
    public ReactiveProperty<int> MusicVolume { get;}
    public ReactiveProperty<int> SFXVolume { get;}

    public GameSettingsStateProxy(GameSettingsState gameSettingState)
    {
        MusicVolume = new ReactiveProperty<int> ( gameSettingState.MusicVolume);
        SFXVolume = new ReactiveProperty<int> ( gameSettingState.SFXVolume);

        MusicVolume.Skip(1).Subscribe(value => gameSettingState.MusicVolume = value);
        SFXVolume.Skip(1).Subscribe(value => gameSettingState.SFXVolume = value);
    }
}