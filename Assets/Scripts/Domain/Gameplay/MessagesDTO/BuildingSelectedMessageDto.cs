using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.MessagesDTO
{
    public struct BuildingSelectedMessageDto
    {
        public BuildingSelectedMessageDto(IBuildingModel buildingModel, int x, int y, object o)
        {
            BuildingModel = buildingModel;
            X = x;
            Y = y;
            Object = o;
        }

        public IBuildingModel BuildingModel { get; }
        public int X { get;  }
        public int Y { get; }
        public object Object { get; }
    }
}