using System;
using Application.Services.MessageService;
using Application.Services.TranscationService;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.GameState;
using MessagePipe;
using Repositories.Building;

namespace Application.UseCases
{
    internal sealed class UpgradeBuildingUseCase : IDisposable
    {
        private readonly IGameStateChanger _gameStateChanger;
        private readonly IPublisher<BuildingUpgradedMessageDto> _buildingUpgradedPublisher;
        private readonly IBuildingsRepository _buildingsRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMessageService _messageService;

        private IDisposable _disposable;
        private IBuildingModel _buildingModel;

        public UpgradeBuildingUseCase(IGameStateChanger gameStateChanger,
            ISubscriber<BuildingSelectedMessageDto> buildingSelectedMessageSubscriber,
            ISubscriber<UpgradeButtonClickedMessageDto> upgradeButtonClickedMessageSubscriber,
            IPublisher<BuildingUpgradedMessageDto> buildingUpgradedPublisher, IBuildingsRepository buildingsRepository,
            ITransactionService transactionService, IMessageService messageService)
        {
            _gameStateChanger = gameStateChanger;
            _buildingUpgradedPublisher = buildingUpgradedPublisher;
            _buildingsRepository = buildingsRepository;
            _transactionService = transactionService;
            _messageService = messageService;

            var bag = DisposableBag.CreateBuilder();

            buildingSelectedMessageSubscriber.Subscribe(OnBuildingSelected).AddTo(bag);
            upgradeButtonClickedMessageSubscriber.Subscribe(OnUpgradeButtonClicked).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnBuildingSelected(BuildingSelectedMessageDto buildingSelectedMessageDto)
        {
            _buildingModel = buildingSelectedMessageDto.BuildingModel;
        }

        private void OnUpgradeButtonClicked(UpgradeButtonClickedMessageDto upgradeButtonClickedMessageDto)
        {
            if (_buildingModel == null || _gameStateChanger.CurrentState != GameState.SelectBuilding)
                return;

            if (_buildingsRepository.TryGetBuilding(_buildingModel.Id, out var repository) == false)
                return;

            if (_transactionService.HasMoneyToSpend(repository.UpgradePrice) == false)
            {
                _messageService.SendMessage("Not enough money to upgrade building!");
                return;
            }

            if (_transactionService.TryRemoveMoney(repository.UpgradePrice) == false)
                return;

            _buildingModel.Upgrade(repository.UpgradeIncomeFactor * _buildingModel.Income);
            _buildingUpgradedPublisher.Publish(new BuildingUpgradedMessageDto());
        }
    }
}