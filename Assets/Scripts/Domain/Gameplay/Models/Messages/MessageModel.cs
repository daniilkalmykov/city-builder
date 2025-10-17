using R3;

namespace Domain.Gameplay.Models.Messages
{
    internal sealed class MessageModel : IMessageModel
    {
        public ReactiveProperty<string> Message { get; } = new();
    }
}