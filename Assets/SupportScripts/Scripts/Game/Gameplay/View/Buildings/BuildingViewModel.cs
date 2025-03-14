using R3;
using UnityEngine;


public class BuildingViewModel
{
    private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingService _buildingService;

    public ReadOnlyReactiveProperty<Vector3Int> Position { get; }
    public readonly int BuildingEntityId;

    /// <summary>
    /// не изменять состояние
    /// </summary>
    /// <param name="buildingEntity"></param>
    /// <param name="buildingService"></param>
    public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingService buildingService)
    {
        BuildingEntityId = buildingEntity.Id;

        _buildingEntity = buildingEntity;
        _buildingService = buildingService;

        Position = buildingEntity.Position;
    }
}