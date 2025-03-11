using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using DI;

public class GameEntryPoint
{
    private static GameEntryPoint _instance;
    private Coroutines _coroutines;
    private UIRootView _uiRoot;
    private readonly DIContainer _rootContainer = new();
    private DIContainer _cachedSceneContainer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
        _instance = new GameEntryPoint();
        _instance.RunGame();
    }

    private GameEntryPoint()
    {
        _coroutines = new GameObject("COROUTINES").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        _uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);
        _rootContainer.RegisterInstance(_uiRoot);

        var gameStateProvider = new PlayerPrefsGameStateProvider();
        gameStateProvider.LoadSettingsState();
        _rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);

        //какойто тестовый сервис ( зачем оно надо пока хз)TODO
        _rootContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();
    }
    private void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == Scenes.GAMEPLAY)
        {
            var enterParams = new GameplayEnterParams("save.asd", 1);
            _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
            return;
        }
        if (sceneName == Scenes.MAIN_MENU)
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
            return;
        }

        if (sceneName != Scenes.BOOT)
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
    {
        _uiRoot.ShowLoadingsScreen();
        _cachedSceneContainer?.Dispose();

        //Сначала загружаеться пустая сцена чтобы точно удалить предыдущую 
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAMEPLAY);

        yield return new WaitForSeconds(1);

        var isGameStateLoaded = false;
        _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
        yield return new WaitUntil(() => isGameStateLoaded);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

        sceneEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParams =>
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
        });

        _uiRoot.HideLoadingsScreen();
    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
    {
        _uiRoot.ShowLoadingsScreen();
        _cachedSceneContainer?.Dispose();

        //Сначала загружаеться пустая сцена чтобы точно удалить предыдущую 
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForSeconds(1);

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        var MainMenuContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

        sceneEntryPoint.Run(MainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;
            if (targetSceneName == Scenes.GAMEPLAY)
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
            }
        });

        _uiRoot.HideLoadingsScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}

