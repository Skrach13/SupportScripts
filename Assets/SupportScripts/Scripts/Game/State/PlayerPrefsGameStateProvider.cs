using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsGameStateProvider : IGameStateProvider
{
    private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
    private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);
    public GameStateProxy GameState { get; private set; }
    public GameSettingsStateProxy SettingsState { get; private set; }

    private GameState _gameStateOrigin;
    private GameSettingsState _gameSettingsStateOrigin;

    public Observable<GameStateProxy> LoadGameState()
    {
        if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
        {
            GameState = CreateGameStateFromSetting();
            Debug.Log("Game State created from setting: " + JsonUtility.ToJson(_gameStateOrigin, true));

            SaveGameState(); // Сохраним деволтное состояние
        }
        else
        {
            // Загружаем
            var json = PlayerPrefs.GetString(GAME_STATE_KEY);
            _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
            GameState = new GameStateProxy(_gameStateOrigin);

            Debug.Log("Game State loaded: " + json);
        }

        return Observable.Return(GameState);
    }
    public Observable<GameSettingsStateProxy> LoadSettingsState()
    {

        if (!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY))
        {
            SettingsState = CreateGameSettingsStateFromSetting();

            SaveSettingsState(); // Сохраним деволтное состояние
        }
        else
        {
            // Загружаем
            var json = PlayerPrefs.GetString(GAME_SETTINGS_STATE_KEY);
            _gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingsState>(json);
            SettingsState = new GameSettingsStateProxy(_gameSettingsStateOrigin);

            Debug.Log("Game State loaded: " + json);
        }

        return Observable.Return(SettingsState);

    }

    public Observable<bool> SaveGameState()
    {
        var json = JsonUtility.ToJson(_gameStateOrigin, true);
        PlayerPrefs.SetString(GAME_STATE_KEY, json);

        return Observable.Return(true);
    }
    public Observable<bool> SaveSettingsState()
    {
        var json = JsonUtility.ToJson(_gameSettingsStateOrigin, true);
        PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

        return Observable.Return(true);
    }

    public Observable<bool> ResetGameState()
    {
        GameState = CreateGameStateFromSetting();
        SaveGameState();

        return Observable.Return(true);
    }
    public Observable<GameSettingsStateProxy> ResetSettingsState()
    {
        SettingsState = CreateGameSettingsStateFromSetting();
        SaveSettingsState();

        return Observable.Return(SettingsState);
    }

    private GameStateProxy CreateGameStateFromSetting()
    {
        //Состояние по умолчанию из настроек, мы делаем фейк
        _gameStateOrigin = new GameState
        {
            Buildings = new List<BuildingEntity>
           {
               new()
               {
                   TypeId = "PRO100"
               },
               new()
               {
                   TypeId = "STARIK"
               }
           }
        };

        return new GameStateProxy(_gameStateOrigin);
    }

   
    private GameSettingsStateProxy CreateGameSettingsStateFromSetting()
    {
        _gameSettingsStateOrigin = new GameSettingsState()
        {
            MusicVolume = 8,
            SFXVolume = 8
        };
        return new GameSettingsStateProxy(_gameSettingsStateOrigin);
    }


}