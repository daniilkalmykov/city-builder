using Domain.Gameplay.Models.Wallet;
using UnityEngine;

namespace Application.Services.TranscationService
{
    internal sealed class InGameWalletTransactionService : ITransactionService
    {
        private readonly IWalletModel _walletModel;

        public InGameWalletTransactionService(IWalletModel walletModel)
        {
            _walletModel = walletModel;
        }

        public bool HasMoneyToSpend(int value)
        {
            return _walletModel.Value.Value >= value;
        }

        public bool TryAddMoney(int value)
        {
            if (value < 0)
                return false;

            _walletModel.Value.Value += value;

            return true;
        }

        public bool TryRemoveMoney(int value)
        {
            if (value < 0 || HasMoneyToSpend(value) == false)
                return false;

            _walletModel.Value.Value -= value;
            return true;
        }
    }
}