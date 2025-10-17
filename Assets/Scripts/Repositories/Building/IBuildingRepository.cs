using UnityEngine;

namespace Repositories.Building
{
    public interface IBuildingRepository
    {
        string Id { get; }
        GameObject Prefab { get; }
        int Income { get; }
        int UpgradeIncomeFactor { get; }
        int Price { get; }
        int UpgradePrice { get; }
    }
}