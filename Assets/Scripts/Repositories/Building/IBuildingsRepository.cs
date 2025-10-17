using System.Collections.Generic;

namespace Repositories.Building
{
    public interface IBuildingsRepository
    {
       IReadOnlyList<IBuildingRepository> Buildings { get; }

       bool TryGetBuilding(string id, out IBuildingRepository buildingRepository);
    }
}