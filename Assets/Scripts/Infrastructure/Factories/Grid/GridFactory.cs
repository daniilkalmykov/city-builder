using System.Runtime.CompilerServices;
using Domain.Gameplay.Models.Grid;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Infrastructure.Factories.Grid
{
    internal sealed class GridFactory : IGridFactory
    {
        public IGridModel CreateGrid(int width, int height, GameObject cellPrefab)
        {
            if (cellPrefab == null)
                return null;

            var parent = new GameObject("Grid");
            
            var cellSize = cellPrefab.transform.localScale.x;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var position = new Vector3(x * cellSize, 0, y * cellSize);
                    var cell = Object.Instantiate(cellPrefab, position, Quaternion.identity, parent.transform);
                    cell.name = $"Cell_{x}_{y}";
                }
            }

            return new GridModel(width, height);
        }
    }
}