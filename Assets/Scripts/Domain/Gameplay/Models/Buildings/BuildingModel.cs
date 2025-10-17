namespace Domain.Gameplay.Models.Buildings
{
    internal sealed class BuildingModel : IBuildingModel
    {
        public BuildingModel(int income, string id)
        {
            Income = income;
            Id = id;
        }

        public int Income { get; private set; }
        public string Id { get; }

        public void Upgrade(int income)
        {
            if (Income >= income)
                return;

            Income = income;
        }
    }
}