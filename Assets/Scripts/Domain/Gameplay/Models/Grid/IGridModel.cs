using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.Models.Grid
{
    public interface IGridModel
    {
        bool IsCellEmpty(int x, int y);
        void OccupyCell(int x, int y, IBuildingModel buildingModel);
        void ClearCell(int x, int y);
        IBuildingModel GetBuildingAt(int x, int y);
    }
}