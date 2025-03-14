using ObservableCollections;

public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<BuildingViewModel> AllBuildings;

    public WorldGameplayRootViewModel(BuildingService buildingService)
    {
        AllBuildings = buildingService.AllBuildings;
    }
}
