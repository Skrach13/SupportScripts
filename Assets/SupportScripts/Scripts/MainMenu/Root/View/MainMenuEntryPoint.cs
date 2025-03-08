using R3;
using UnityEngine;


public class MainMenuEntryPoint : MonoBehaviour
{

    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public Observable<MainMenuExitParams> Run(UIRootView uiRoot, MainMenuEnterParams enterParams)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSgnalSubj = new Subject<Unit>();
        uiScene.Bind(exitSgnalSubj);

        Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Result {enterParams?.Result}");

        //Test TODO
        var saveFileName = "ololo.save";
        var levelNumber = Random.Range(0, 300);
        //
        var gamplayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
        var mainMenuExitParams = new MainMenuExitParams(gamplayEnterParams);
        var exitToGameplaySceneSignal = exitSgnalSubj.Select(_ => mainMenuExitParams);

        return exitToGameplaySceneSignal;
    }
}
