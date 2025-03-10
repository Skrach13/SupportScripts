using UnityEditor;

public class UIGameplayRootViewModel
{
    public readonly SomeGameplayService _someGameplayService;

    public UIGameplayRootViewModel(SomeGameplayService someGameplayService)
    {
        _someGameplayService = someGameplayService;
    }
}
