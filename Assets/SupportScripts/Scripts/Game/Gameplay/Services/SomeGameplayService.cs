using ObservableCollections;
using System;
using UnityEngine;
using R3;
using System.Linq;

public class SomeGameplayService : IDisposable
{
    private readonly GameStateProxy _gameState;
    private readonly SomeCommonService _someCommonService;

    public SomeGameplayService(GameStateProxy gameState, SomeCommonService someCommonService)
    {
        this._gameState = gameState;
        _someCommonService = someCommonService;
        Debug.Log(GetType().Name + "has been created");



        //gameState.Buildings.ForEach(b => Debug.Log($"Building: {b.TypeId}"));
        //gameState.Buildings.ObserveAdd().Subscribe(e => Debug.Log($"Building add: {e.Value.TypeId}"));
        //gameState.Buildings.ObserveRemove().Subscribe(e => Debug.Log($"Building removed: {e.Value.TypeId}"));

        ////Test TODO
        //AddBuilding("Афсян");
        //AddBuilding("Атасян");
        //AddBuilding("Атукун");

        //RemoveBuilding("Атасян");
        ////
    }

    public void Dispose()
    {
        Debug.Log("Подчистить все подписки");
    }

    private void AddBuilding(string buildingTypeId)
    {
        var building = new BuildingEntity
        {
            TypeId = buildingTypeId,
        };
        var buildingProxy = new BuildingEntityProxy(building);
        _gameState.Buildings.Add(buildingProxy);
    }
    private void RemoveBuilding(string buildingTypeId)
    {
        var buildingEnitity = _gameState.Buildings.FirstOrDefault(b => b.TypeId == buildingTypeId);

        if (buildingEnitity != null)
        {
            _gameState.Buildings.Remove(buildingEnitity);
        }
    }

}
