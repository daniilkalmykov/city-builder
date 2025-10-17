using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Repositories.Grid
{
    [CreateAssetMenu(fileName = "GridRepository", menuName = "Repositories/GridRepository")]
    internal sealed class GridRepository : ScriptableObject, IGridRepository
    {
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField] public GameObject CellPrefab { get; private set; }
    }
}