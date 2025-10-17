namespace Application.Services.TranscationService
{
    public interface ITransactionService
    {
        bool HasMoneyToSpend(int value);
        bool TryAddMoney(int value);
        bool TryRemoveMoney(int value);
    }
}