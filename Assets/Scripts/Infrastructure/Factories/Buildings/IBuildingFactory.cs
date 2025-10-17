using Domain.Gameplay.Models.Buildings;
using UnityEngine;

namespace Infrastructure.Factories.Buildings
{
    public interface IBuildingFactory
    {
        IBuildingModel CreateBuilding(string id, Vector3 position);
    }
}