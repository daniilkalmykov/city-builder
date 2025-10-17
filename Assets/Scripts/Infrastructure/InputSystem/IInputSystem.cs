namespace Infrastructure.InputSystem
{
    public interface IInputSystem
    {
        float Horizontal { get; }
        float Vertical { get; }
        float Scroll { get; }
    }
}