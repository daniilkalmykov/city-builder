using System;
using System.Runtime.CompilerServices;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Grid;
using Infrastructure.Factories;
using Infrastructure.Factories.Grid;
using MessagePipe;
using Repositories.Grid;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Application.UseCases
{
    internal sealed class SpawnGridUseCase : IDisposable
    {
        private readonly IGridRepository _gridRepository;
        private readonly IGridFactory _factory;
        private readonly IPublisher<GridInitializedMessageDto> _gridInitializedPublisher;
        
        private IGridModel _gridModel;
        private IDisposable _disposable;

        public SpawnGridUseCase(ISubscriber<GameLoadedMessageDto> gameLoadedSubscriber, IGridRepository gridRepository,
            IGridFactory factory, IPublisher<GridInitializedMessageDto> gridInitializedPublisher)
        {
            _gridRepository = gridRepository;
            _factory = factory;
            _gridInitializedPublisher = gridInitializedPublisher;

            var bag = DisposableBag.CreateBuilder();

            gameLoadedSubscriber.Subscribe(OnGameLoaded).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnGameLoaded(GameLoadedMessageDto gameLoadedMessageDto)
        {
            SpawnGrid();
        }

        private void SpawnGrid()
        {
            _gridModel = _factory.CreateGrid(_gridRepository.Width, _gridRepository.Height, _gridRepository.CellPrefab);
            
            _gridInitializedPublisher.Publish(new GridInitializedMessageDto(_gridModel));
        }
    }
}