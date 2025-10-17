using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public struct GridInitializedMessageDto
    {
        public GridInitializedMessageDto(IGridModel gridModel)
        {
            GridModel = gridModel;
        }

        public IGridModel GridModel { get; }
    }
}