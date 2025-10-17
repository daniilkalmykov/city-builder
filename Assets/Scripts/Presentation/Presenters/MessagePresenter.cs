using System;
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

        public MessagePresenter(IMessageView messageView, IMessageModel messageModel)
        {
            _messageView = messageView;
            _messageModel = messageModel;
        }

        public void Initialize()
        {
            _disposable = _messageModel.Message.Subscribe(async message =>
            {
                _messageView.Show(message);
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                
            });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }
    }
}