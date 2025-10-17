using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.GameState;
using MessagePipe;

namespace Application.UseCases
{
    internal sealed class ChangeGameStateUseCase : IDisposable
    {
        private readonly IGameStateChanger _gameStateChanger;

        private IDisposable _disposable;

        public ChangeGameStateUseCase(IGameStateChanger gameStateChanger,
            ISubscriber<BuildingSelectedMessageDto> buildingSelectedMessageSubscriber,
            ISubscriber<BuildingDeletedMessageDto> buildingDeletedMessageSubscriber,
            ISubscriber<BuildingMovedMessageDto> buildingMovedMessageSubscriber,
            ISubscriber<CancelOperationButtonClickedMessageDto> cancelOperationButtonClickedSubscriber,
            ISubscriber<BuildingChosenMessageDto> buildingChosenMessageSubscriber)
        {
            _gameStateChanger = gameStateChanger;

            var bag = DisposableBag.CreateBuilder();

            buildingSelectedMessageSubscriber.Subscribe(OnBuildingSelected).AddTo(bag);
            buildingDeletedMessageSubscriber.Subscribe(OnBuildingDeleted).AddTo(bag);
            buildingMovedMessageSubscriber.Subscribe(OnBuildingMoved).AddTo(bag);
            cancelOperationButtonClickedSubscriber.Subscribe(OnCancelOperationButtonClicked).AddTo(bag);
            buildingChosenMessageSubscriber.Subscribe(OnBuildingChosen).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnBuildingSelected(BuildingSelectedMessageDto buildingSelectedMessageDto)
        {
            if (buildingSelectedMessageDto.BuildingModel != null)
                _gameStateChanger.Change(GameState.SelectBuilding);
        }

        private void OnBuildingDeleted(BuildingDeletedMessageDto buildingDeletedMessageDto)
        {
            _gameStateChanger.Change(GameState.Idle);
        }

        private void OnBuildingMoved(BuildingMovedMessageDto buildingMovedMessageDto)
        {
            _gameStateChanger.Change(GameState.Idle);
        }
        
        private void OnCancelOperationButtonClicked(CancelOperationButtonClickedMessageDto cancelOperationButtonClickedMessageDto)
        {
            _gameStateChanger.Change(GameState.Idle);
        }

        private void OnBuildingChosen(BuildingChosenMessageDto buildingChosenMessageDto)
        {
            if (buildingChosenMessageDto.Building != null)
                _gameStateChanger.Change(GameState.Building);
        }
    }
}