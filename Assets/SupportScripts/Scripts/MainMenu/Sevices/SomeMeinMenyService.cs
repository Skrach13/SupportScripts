using UnityEngine;

public class SomeMeinMenyService
{
    private readonly SomeCommonService _someCommonService;

    public SomeMeinMenyService(SomeCommonService someCommonService)
    {
        _someCommonService = someCommonService;
        Debug.Log(GetType().Name + " has been created");
    }
}
