﻿using ObservableCollections;
using R3;
using System.Linq;

public class GameStateProxy
{
    private readonly GameState _gameState;

    public ObservableList<BuildingEntityProxy> Buildings { get; } = new();


    public GameStateProxy(GameState gameState)
    {
        _gameState = gameState;
        gameState.Buildings.ForEach(buildingOrigin => Buildings.Add(new BuildingEntityProxy(buildingOrigin)));
    
        Buildings.ObserveAdd().Subscribe(e =>
        {
            var addedBuildingEntity = e.Value;
            gameState.Buildings.Add(addedBuildingEntity.Origin);
        });

        Buildings.ObserveRemove().Subscribe(e =>
        {
            var removedBuildingEntityProxy = e.Value;
            var removedBuildingEntity = gameState.Buildings.FirstOrDefault(b => b.Id == removedBuildingEntityProxy.Id);
            gameState.Buildings.Remove(removedBuildingEntity);
        });
        this._gameState = gameState;
    }

    public int GatEntityId()
    {
        return _gameState.GloabalEntityId++;
    }
}