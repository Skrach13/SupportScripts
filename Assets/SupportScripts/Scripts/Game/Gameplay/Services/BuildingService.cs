using ObservableCollections;
using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

public class BuildingService
{
    private readonly ICommandProcessor _cmd;
    private readonly ObservableList<BuildingViewModel> _allBuildings = new();
    private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();

    public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;

    public BuildingService(IObservableCollection<BuildingEntityProxy> buildings, ICommandProcessor cmd)
    {
        _cmd = cmd;

        foreach (var buildingEntity in buildings)
        {
            CreateBuildingViewModel(buildingEntity);
        }

        buildings.ObserveAdd().Subscribe(e =>
        {
            CreateBuildingViewModel(e.Value);
        });

        buildings.ObserveRemove().Subscribe(e =>
        {
            RemoveBuildingViewModel(e.Value);
        });
    }

    public bool PlaceBuilding(string builngTypeId, Vector3Int position)
    {
        var command = new CmdPlaceBuilding(builngTypeId, position);
        var result = _cmd.Process(command);

        return result;
    }

    public bool MoveBuilding(int buildingEntityId, Vector3Int position)
    {
        throw new NotImplementedException();
    }
    public bool DeleteBuilding(int buildingEntityId)
    {
        throw new NotImplementedException();
    }

    private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
    {
        var buildingViewModel = new BuildingViewModel(buildingEntity, this);

        _allBuildings.Add(buildingViewModel);
        _buildingsMap[buildingEntity.Id] = buildingViewModel;
    }

    private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
    {
        if (_buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
        {
            _allBuildings.Remove(buildingViewModel);
            _buildingsMap.Remove(buildingEntity.Id);
        }
    }

}
