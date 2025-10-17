using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.MessagesDTO
{
    public struct BuildingDeletedMessageDto
    {
        public BuildingDeletedMessageDto(IBuildingModel buildingModel)
        {
            BuildingModel = buildingModel;
        }

        public IBuildingModel BuildingModel { get; private set; }
    }
}