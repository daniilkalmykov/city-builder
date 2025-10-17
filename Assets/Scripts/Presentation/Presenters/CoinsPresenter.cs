using System;
using System.Runtime.CompilerServices;
using Domain.Gameplay.Models.Wallet;
using Presentation.Views.CoinsView;
using R3;
using VContainer.Unity;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Presentation.Presenters
{
    internal sealed class CoinsPresenter : IDisposable, IInitializable
    {
        private readonly IWalletModel _walletModel;
        private readonly ICoinsView _coinsView;

        private IDisposable _disposable;

        public CoinsPresenter(IWalletModel walletModel, ICoinsView coinsView)
        {
            _walletModel = walletModel;
            _coinsView = coinsView;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        public void Initialize()
        {
            _disposable = _walletModel.Value.Subscribe(value =>
            {
                _coinsView.UpdateCoinCount(value);
            });
        }
    }
}