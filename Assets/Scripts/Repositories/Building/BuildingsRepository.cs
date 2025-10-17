using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Repositories.Building
{
    [CreateAssetMenu(fileName = "BuildingsRepository", menuName = "Repositories/BuildingsRepository")]
    internal sealed class BuildingsRepository : ScriptableObject, IBuildingsRepository
    {
        [SerializeField] private BuildingRepository[] _buildingRepositories;

        public IReadOnlyList<IBuildingRepository> Buildings => _buildingRepositories;
        
        public bool TryGetBuilding(string id, out IBuildingRepository buildingRepository)
        {
            buildingRepository = _buildingRepositories.FirstOrDefault(repository => repository.Id == id);

            return buildingRepository != null;
        }
    }
}