using Domain.Gameplay.Models.Grid;
using UnityEngine;

namespace Infrastructure.Factories.Grid
{
    public interface IGridFactory
    {
        IGridModel CreateGrid(int width, int height, GameObject cellPrefab);
    }
}