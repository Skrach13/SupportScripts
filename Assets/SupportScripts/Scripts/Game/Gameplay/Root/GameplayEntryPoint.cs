using DI;
using ObservableCollections;
using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

    public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
    {
        GameplayRegestration.Register(gameplayContainer, enterParams);
        var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
        GameplayViewModelRegistration.Register(gameplayViewModelsContainer);

        var gameStateProvider = gameplayContainer.Resolve<IGameStateProvider>();

        ///
        ///
        gameStateProvider.GameState.Buildings.ObserveAdd().Subscribe(e => {
            
            var building = e.Value;
            Debug.Log("Building placed. Type id: " +
                building.TypeId
                + " Id: " + building.Id
                + ", Position: " + building.Position.Value);

        });
        //TEST

        var cmd = new CommandProcessor(gameStateProvider);

        cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameStateProvider.GameState));

        ///
        cmd.Proceess(new CmdPlaceBuilding("Васян", GetRandomPosition()));
        cmd.Proceess(new CmdPlaceBuilding("Dasd", GetRandomPosition()));
        cmd.Proceess(new CmdPlaceBuilding("Skrach", GetRandomPosition()));
        ///

        gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
        gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>();
        //

        var uiRoot = gameplayContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        Debug.Log($"GAMEPLAT ENTRY POINT: save file name = {enterParams.SaveFileName}, level to load = {enterParams.LevelNumber}");

        var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
        var exitParams = new GameplayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }

    private Vector3Int GetRandomPosition()
    {
        var rX = Random.Range(-10, 10);
        var rY = Random.Range(-10, 10);
        var rPosition = new Vector3Int(rX, rY, 0);
        return rPosition;
    }
}
