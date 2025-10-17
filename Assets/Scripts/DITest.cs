using Application.Services.MessageService;
using Application.Services.RaycastService;
using Application.Services.TranscationService;
using Application.UseCases;
using Domain.Gameplay.Models.GameState;
using Domain.Gameplay.Models.Wallet;
using Infrastructure.Factories.Buildings;
using Infrastructure.Factories.Grid;
using Infrastructure.InputSystem;
using Infrastructure.PlayerRepository;
using MessagePipe;
using Presentation.Presenters;
using Presentation.Views.CoinsView;
using Presentation.Views.MessageView;
using Repositories.Building;
using Repositories.Camera;
using Repositories.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DITest : LifetimeScope
{
    [SerializeField] private GridRepository _gridRepository;
    [SerializeField] private BuildingsRepository _buildingRepository;
    [SerializeField] private CameraRepository _cameraRepository;
    [SerializeField] private PlayerRepository _playerRepository;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterMessagePipe();

        builder.Register<IGameStateChanger, GameStateChanger>(Lifetime.Singleton);
        builder.Register<IGridFactory, GridFactory>(Lifetime.Singleton);
        builder.Register<IBuildingFactory, BuildingFactory>(Lifetime.Singleton);
        builder.Register<IWalletModel, WalletModel>(Lifetime.Singleton);
        builder.Register<SpawnGridUseCase>(Lifetime.Singleton);
        builder.Register<SpawnBuildingUseCase>(Lifetime.Singleton);
        builder.Register<SelectBuildingUseCase>(Lifetime.Singleton);
        builder.Register<DeleteBuildingUseCase>(Lifetime.Singleton);
        builder.Register<ChangeGameStateUseCase>(Lifetime.Singleton);
        builder.Register<ChooseBuildingUseCase>(Lifetime.Singleton);
        builder.Register<MoveBuildingUseCase>(Lifetime.Singleton);
        builder.Register<UpgradeBuildingUseCase>(Lifetime.Singleton);
        builder.Register<IncomeUseCase>(Lifetime.Singleton);
        builder.Register<IRaycastService, RaycastService>(Lifetime.Singleton);
        builder.Register<IInputSystem, ITickable, PCInputSystem>(Lifetime.Singleton);
        builder.Register<ITickable, CameraMovementUseCase>(Lifetime.Singleton);
        builder.Register<ITickable, CameraZoomUseCase>(Lifetime.Singleton);
        builder.RegisterInstance<IGridRepository>(_gridRepository);
        builder.RegisterInstance<IBuildingsRepository>(_buildingRepository);
        builder.RegisterInstance<ICameraRepository>(_cameraRepository);
        builder.RegisterInstance<IPlayerRepository>(_playerRepository);
        builder.Register<ITransactionService, InGameWalletTransactionService>(Lifetime.Singleton);
        builder.Register<AddStartMoneyUseCase>(Lifetime.Singleton);
        builder.Register<IInitializable, CoinsPresenter>(Lifetime.Singleton);
        builder.Register<MessagePresenter>(Lifetime.Singleton);
        builder.Register<IMessageService, MessageService>(Lifetime.Singleton);
        
        builder.RegisterComponentInHierarchy<Test2>();
        builder.RegisterComponentInHierarchy<Camera>();
        builder.RegisterComponentInHierarchy<ICoinsView>();
        builder.RegisterComponentInHierarchy<IMessageView>();
        
        builder.RegisterBuildCallback(container =>
        {
            container.Resolve<SpawnGridUseCase>();
            container.Resolve<SpawnBuildingUseCase>();
            container.Resolve<SelectBuildingUseCase>();
            container.Resolve<DeleteBuildingUseCase>();
            container.Resolve<DeleteBuildingUseCase>();
            container.Resolve<ChangeGameStateUseCase>();
            container.Resolve<MoveBuildingUseCase>();
            container.Resolve<UpgradeBuildingUseCase>();
            container.Resolve<IncomeUseCase>();
            container.Resolve<AddStartMoneyUseCase>();
            container.Resolve<ChooseBuildingUseCase>();
            container.Resolve<MessagePresenter>();
        });

    }
}