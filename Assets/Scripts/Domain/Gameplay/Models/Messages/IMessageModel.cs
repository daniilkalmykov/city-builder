using R3;

namespace Domain.Gameplay.Models.Messages
{
    public interface IMessageModel
    {
        ReactiveProperty<string> Message { get; }
    }
}