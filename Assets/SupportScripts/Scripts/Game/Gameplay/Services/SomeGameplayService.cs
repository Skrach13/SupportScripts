using System;
using UnityEngine;

public class SomeGameplayService : IDisposable
{
    private readonly SomeCommonService _someCommonService;

    public SomeGameplayService(SomeCommonService someCommonService)
    {
        _someCommonService = someCommonService;
        Debug.Log(GetType().Name + "has been created");
    }

    public void Dispose()
    {
        Debug.Log("Подчистить все подписки");
    }
}
