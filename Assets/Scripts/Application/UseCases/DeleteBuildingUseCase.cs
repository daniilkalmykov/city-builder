using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using UnityEngine;

namespace Application.UseCases
{
    internal sealed class DeleteBuildingUseCase : IDisposable
    {
        private readonly IPublisher<BuildingDeletedMessageDto> _buildingDeletedMessagePublisher;

        private IDisposable _disposable;
        private IBuildingModel _buildingModel;
        private int _x;
        private int _y;
        private GameObject _gameObject;
        private IGridModel _gridModel;

        public DeleteBuildingUseCase(ISubscriber<BuildingSelectedMessageDto> buildingSelectedMessageSubscriber,
            ISubscriber<DeleteButtonTouchedMessageDto> deleteButtonTouchedMessageSubscriber,
            ISubscriber<GridInitializedMessageDto> gridInitializedSubscriber,
            IPublisher<BuildingDeletedMessageDto> buildingDeletedMessagePublisher)
        {
            _buildingDeletedMessagePublisher = buildingDeletedMessagePublisher;
            var bag = DisposableBag.CreateBuilder();

            buildingSelectedMessageSubscriber.Subscribe(OnBuildingSelected).AddTo(bag);
            deleteButtonTouchedMessageSubscriber.Subscribe(OnDeleteButtonTouched).AddTo(bag);
            gridInitializedSubscriber.Subscribe(OnGridInitialized).AddTo(bag);

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

            if (_buildingModel == null)
                return;

            _x = buildingSelectedMessageDto.X;
            _y = buildingSelectedMessageDto.Y;
            _gameObject = (GameObject)buildingSelectedMessageDto.Object;
        }

        private void OnDeleteButtonTouched(DeleteButtonTouchedMessageDto deleteButtonTouchedMessageDto)
        {
            if (_buildingModel == null)
                return;

            _gridModel.ClearCell(_x, _y);
            UnityEngine.Object.Destroy(_gameObject);
            _buildingDeletedMessagePublisher.Publish(new BuildingDeletedMessageDto(_buildingModel));
        }

        private void OnGridInitialized(GridInitializedMessageDto gridInitializedMessageDto)
        {
            _gridModel = gridInitializedMessageDto.GridModel;
        }
    }
}