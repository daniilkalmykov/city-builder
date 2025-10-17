using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Views.MessageView;

namespace Presentation.Presenters
{
    internal sealed class MessagePresenter : IDisposable
    {
        private readonly IMessageView _messageView;

        private IDisposable _disposable;
        private CancellationTokenSource _cancellationTokenSource;

        public MessagePresenter(IMessageView messageView, ISubscriber<MessageSentMessageDto> messageSentSubscriber)
        {
            _messageView = messageView;
            
            var bag = DisposableBag.CreateBuilder();

            messageSentSubscriber.Subscribe(OnMessageSent).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void OnMessageSent(MessageSentMessageDto message)
        {
            ShowMessageAsync(message.Message);
        }
        
        private async void ShowMessageAsync(string message)
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                _messageView.Show(message);

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _cancellationTokenSource.Token);

                _messageView.Hide();
            }
            catch (Exception e)
            {
                throw new Exception("Error showing message", e);
            }
        }
    }
}