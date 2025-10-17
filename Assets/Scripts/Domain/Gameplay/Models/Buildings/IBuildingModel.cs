namespace Domain.Gameplay.Models.Buildings
{
    public interface IBuildingModel
    {
        string Id { get; }
        int Income { get; }
        void Upgrade(int income);
    }
}