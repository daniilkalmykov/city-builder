using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.MessagesDTO
{
    public struct BuildingSpawnedMessageDto
    {
        public BuildingSpawnedMessageDto(IBuildingModel buildingModel)
        {
            BuildingModel = buildingModel;
        }

        public IBuildingModel BuildingModel { get; }
    }
}