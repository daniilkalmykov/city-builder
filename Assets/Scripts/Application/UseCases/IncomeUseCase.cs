using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Application.Services.TranscationService;
using Cysharp.Threading.Tasks;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using MessagePipe;
using UnityEngine;

namespace Application.UseCases
{
    internal sealed class IncomeUseCase : IDisposable
    {
        private readonly List<IBuildingModel> _buildingModels = new();
        private readonly ITransactionService _transactionService;

        private IDisposable _disposable;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;

        public IncomeUseCase(
            ISubscriber<BuildingSpawnedMessageDto> buildingSpawnedMessageSubscriber,
            ISubscriber<BuildingDeletedMessageDto> buildingDeletedMessageSubscriber,
            ITransactionService transactionService)
        {
            _transactionService = transactionService;
            var bag = DisposableBag.CreateBuilder();

            buildingSpawnedMessageSubscriber.Subscribe(OnBuildingSpawned).AddTo(bag);
            buildingDeletedMessageSubscriber.Subscribe(OnBuildingDeleted).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            StopIncomeLoop();
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnBuildingSpawned(BuildingSpawnedMessageDto message)
        {
            _buildingModels.Add(message.BuildingModel);

            if (_isRunning == false)
                StartIncomeLoop();
        }

        private void OnBuildingDeleted(BuildingDeletedMessageDto message)
        {
            _buildingModels.Remove(message.BuildingModel);

            if (_buildingModels.Count == 0)
                StopIncomeLoop();
        }

        private void StartIncomeLoop()
        {
            if (_isRunning)
                return;

            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = true;
            
            CalculateIncome(_cancellationTokenSource.Token).Forget();
        }

        private void StopIncomeLoop()
        {
            if (_isRunning == false)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            
            _cancellationTokenSource = null;
            _isRunning = false;
        }

        private async UniTask CalculateIncome(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (_buildingModels.Count == 0)
                        break;

                    var totalIncome = _buildingModels.Sum(b => b.Income);

                    if (_transactionService.TryAddMoney(totalIncome) == false)
                    {
                        Debug.LogError("Money adding failed");
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
