using R3;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubj;
    public void HandleGoToGameplayButtonLick()
    {
        _exitSceneSignalSubj?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        _exitSceneSignalSubj = exitSceneSignalSubj;
    }
}
