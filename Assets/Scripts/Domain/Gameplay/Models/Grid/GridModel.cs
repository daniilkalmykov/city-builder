using System.Runtime.CompilerServices;
using Domain.Gameplay.Models.Buildings;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
[assembly: InternalsVisibleTo("Infrastructure")]
[assembly: InternalsVisibleTo("Application")]
namespace Domain.Gameplay.Models.Grid
{
    internal sealed class GridModel : IGridModel
    {
        private readonly IBuildingModel[,] _cells;

        public GridModel(int width, int height)
        {
            _cells = new IBuildingModel[width, height];
        }

        public bool IsCellEmpty(int x, int y)
        {
            return _cells[x, y] == null;
        }

        public void OccupyCell(int x, int y, IBuildingModel buildingModel)
        {
            _cells[x, y] = buildingModel;
        }

        public void ClearCell(int x, int y)
        {
            _cells[x, y] = null;
        }

        public IBuildingModel GetBuildingAt(int x, int y)
        {
            return _cells[x, y];
        }
    }
}