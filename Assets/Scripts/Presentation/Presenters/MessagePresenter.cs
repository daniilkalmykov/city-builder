using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.Gameplay.Models.Messages;
using Presentation.Views.MessageView;
using R3;
using VContainer.Unity;

namespace Presentation.Presenters
{
    internal sealed class MessagePresenter : IDisposable, IInitializable
    {
        private readonly IMessageView _messageView;
        private readonly IMessageModel _messageModel;

        private IDisposable _disposable;
        private CancellationTokenSource _cts;

        public MessagePresenter(IMessageView messageView, IMessageModel messageModel)
        {
            _messageView = messageView;
            _messageModel = messageModel;
        }

        public void Initialize()
        {
            _disposable = _messageModel.Message.Subscribe(ShowMessageAsync);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private async void ShowMessageAsync(string message)
        {
            try
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                _messageView.Show(message);

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _cts.Token);

                _messageView.Hide();
            }
            catch (Exception e)
            {
                throw new Exception("Error showing message", e);
            }
        }
    }
}