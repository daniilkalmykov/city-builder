using UnityEngine;

namespace Repositories.Grid
{
    public interface IGridRepository
    {
        int Width { get; }
        int Height { get; }
        GameObject CellPrefab { get; }
    }
}