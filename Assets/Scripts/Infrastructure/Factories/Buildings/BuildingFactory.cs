using Domain.Gameplay.Models.Buildings;
using Repositories.Building;
using UnityEngine;

namespace Infrastructure.Factories.Buildings
{
    internal sealed class BuildingFactory : IBuildingFactory
    {
        private readonly IBuildingsRepository _buildingsRepository;

        public BuildingFactory(IBuildingsRepository buildingsRepository)
        {
            _buildingsRepository = buildingsRepository;
        }

        public IBuildingModel CreateBuilding(string id, Vector3 position)
        {
            if (_buildingsRepository.TryGetBuilding(id, out var buildingRepository) == false)
            {
                return null;
            }

            Object.Instantiate(buildingRepository.Prefab, position, Quaternion.identity);

            return new BuildingModel(buildingRepository.Income, id);
        }
    }
}