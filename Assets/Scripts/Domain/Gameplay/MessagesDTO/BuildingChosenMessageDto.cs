using Repositories.Building;

namespace Domain.Gameplay.MessagesDTO
{
    public struct BuildingChosenMessageDto
    {
        public BuildingChosenMessageDto(IBuildingRepository building)
        {
            Building = building;
        }

        public IBuildingRepository Building { get; }
    }
}