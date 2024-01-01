using Myafim.Domain.Errors;
using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Myafim.Domain.Requests;
using Myafim.Utils;

namespace Myafim.Domain.Handlers;

public class CreateTransactionHandler(
    ITransactionsRepository transactionsRepository,
    ICategoriesRepository categoriesRepository,
    IAccountsRepository accountsRepository)
{
    public async Task<Either<List<TransactionError>, Transaction>> HandleAsync(CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var transactionResult = await MakeTransactionAsync(request, cancellationToken);

        return transactionResult.TryGetRight(out var transaction, out var errors)
            ? await transactionsRepository.CreateAsync(transaction, cancellationToken)
            : errors;
    }

    private async Task<Either<List<TransactionError>, Transaction>> MakeTransactionAsync(
        CreateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        var errors = new List<TransactionError>();

        if (request.Amount <= 0)
            errors.Add(new TransactionError.AmountMustBeStrictlyPositive());
        if (request.Description?.Length > 80)
            errors.Add(new TransactionError.DescriptionMustBeLessThan80Characters());
        if (request.SourceAccountId == request.DestinationAccountId)
            errors.Add(new TransactionError.SourceAndDestinationAccountsMustBeDifferent());

        var (sourceAccount, destinationAccount) = await RetrieveAccountsAsync(request, cancellationToken);
        if (sourceAccount is null)
            errors.Add(new TransactionError.SourceAccountDoesNotExist());
        if (destinationAccount is null)
            errors.Add(new TransactionError.DestinationAccountDoesNotExist());

        if (request.CategoryId is not null)
        {
            var category = await categoriesRepository.GetByIdAsync(request.CategoryId.Value, cancellationToken);
            if (category is null)
                errors.Add(new TransactionError.CategoryDoesNotExist());
        }

        if (errors.Count != 0)
        {
            return errors;
        }
        
        return new Transaction
        {
            Description = request.Description,
            Amount = request.Amount,
            ValueDate = request.ValueDate,
            SourceAccountId = request.SourceAccountId,
            DestinationAccountId = request.DestinationAccountId,
            CategoryId = request.CategoryId
        };
    }

    private async Task<(Account? SourceAccount, Account? DestinationAccount)> RetrieveAccountsAsync(
        CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var accountsFilters = new AccountsFilters
        {
            Ids = [request.SourceAccountId, request.DestinationAccountId]
        };
        var accounts = await accountsRepository.ListRawAsync(accountsFilters, cancellationToken);
        return (
            SourceAccount: accounts.SingleOrDefault(a => a.Id == request.SourceAccountId),
            DestinationAccount: accounts.SingleOrDefault(a => a.Id == request.DestinationAccountId)
        );
    }
}