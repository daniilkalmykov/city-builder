namespace Repositories.Camera
{
    public interface ICameraRepository
    {
        float MoveSpeed { get; }
        float MinFov { get; }
        float MaxFov { get; }
        float ZoomSpeed { get; }
    }
}