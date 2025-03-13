public class BuildingViewModel
{
    private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingService _buildingService;

    /// <summary>
    /// не изменять состояние
    /// </summary>
    /// <param name="buildingEntity"></param>
    /// <param name="buildingService"></param>
    public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingService buildingService)
    {
        _buildingEntity = buildingEntity;
        _buildingService = buildingService;
    }
}