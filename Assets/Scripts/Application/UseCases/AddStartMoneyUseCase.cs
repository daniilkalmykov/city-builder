using System;
using Application.Services.TranscationService;
using Domain.Gameplay.MessagesDTO;
using Infrastructure.PlayerRepository;
using MessagePipe;

namespace Application.UseCases
{
    internal sealed class AddStartMoneyUseCase : IDisposable
    {
        private readonly ITransactionService _transactionService;
        private readonly IPlayerRepository _playerRepository;

        private IDisposable _disposable;

        public AddStartMoneyUseCase(ITransactionService transactionService, IPlayerRepository playerRepository, 
            ISubscriber<GridInitializedMessageDto> gridInitializedSubscriber)
        {
            _transactionService = transactionService;
            _playerRepository = playerRepository;

            var bag = DisposableBag.CreateBuilder();
            
            gridInitializedSubscriber.Subscribe(OnGridInitialized).AddTo(bag);
            
            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnGridInitialized(GridInitializedMessageDto message)
        {
            if (_transactionService.TryAddMoney(_playerRepository.StartMoney) == false)
                throw new Exception("Failed to add start money.");
        }
    }
}