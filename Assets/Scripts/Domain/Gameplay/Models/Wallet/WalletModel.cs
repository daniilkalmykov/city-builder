using R3;

namespace Domain.Gameplay.Models.Wallet
{
    internal sealed class WalletModel : IWalletModel
    {
        public ReactiveProperty<int> Value { get; } = new();
    }
}