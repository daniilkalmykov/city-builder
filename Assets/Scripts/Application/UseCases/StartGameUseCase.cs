using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using VContainer.Unity;

namespace Application.UseCases
{
    internal sealed class StartGameUseCase : IStartable
    {
        private readonly IPublisher<GameLoadedMessageDto> _publisher;

        public StartGameUseCase(IPublisher<GameLoadedMessageDto> publisher)
        {
            _publisher = publisher;
        }

        public void Start()
        {
            _publisher.Publish(new GameLoadedMessageDto());
        }
    }
}