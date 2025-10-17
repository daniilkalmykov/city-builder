using UnityEngine;

namespace Repositories.Building
{
    [CreateAssetMenu(fileName = "BuildingRepository", menuName = "Repositories/BuildingRepository")]
    internal sealed class BuildingRepository : ScriptableObject, IBuildingRepository
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int Income { get; private set; }
        [field: SerializeField] public int UpgradeIncomeFactor { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public int UpgradePrice { get; private set; }
    }
}