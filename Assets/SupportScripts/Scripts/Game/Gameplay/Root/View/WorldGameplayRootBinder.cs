using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;

public class WorldGameplayRootBinder : MonoBehaviour
{
    [SerializeField] private BuildingBinder _prefabBuilding;

    private readonly Dictionary<int, BuildingBinder> _createBuildingMap = new();

    private readonly CompositeDisposable _disposable = new ();

    public void Bind(WorldGameplayRootViewModel viewModel)
    {
        foreach (var buildingViewModel in viewModel.AllBuildings)
        {
            CreateBuilding(buildingViewModel);
        }
        _disposable.Add(viewModel.AllBuildings.ObserveAdd()
            .Subscribe(e => CreateBuilding(e.Value)));

        _disposable.Add(viewModel.AllBuildings.ObserveRemove()
            .Subscribe(e => DestoryBuilding(e.Value)));
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }

    private void CreateBuilding(BuildingViewModel buildingViewModel)
    {
        var createdBuilding = Instantiate(_prefabBuilding);
        createdBuilding.Bind(buildingViewModel);

        _createBuildingMap[buildingViewModel.BuildingEntityId] = createdBuilding;
    }

    private void DestoryBuilding(BuildingViewModel buildingViewModel) 
    {
        if(_createBuildingMap.TryGetValue(buildingViewModel.BuildingEntityId, out var buildingBinder))
        {
            Destroy(buildingBinder.gameObject);
            _createBuildingMap.Remove(buildingViewModel.BuildingEntityId);
        }
    }

}