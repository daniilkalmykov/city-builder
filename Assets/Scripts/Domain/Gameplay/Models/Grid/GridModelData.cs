using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.Models.Grid
{
    internal readonly struct GridModelData
    {
        public readonly BuildingModel[,] BuildingModels;

        public GridModelData(BuildingModel[,] buildingModels)
        {
            BuildingModels = buildingModels;
        }
    }
}