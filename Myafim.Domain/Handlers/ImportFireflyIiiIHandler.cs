using Myafim.Domain.Repositories;
using Myafim.FireflyIii.Client;
using Account = Myafim.Domain.Models.Account;
using Category = Myafim.Domain.Models.Category;
using Transaction = Myafim.Domain.Models.Transaction;

namespace Myafim.Domain.Handlers;

public class ImportFireflyIiiIHandler(
    FireflyIiiClientFactory fireflyIiiClientFactory,
    IUnitOfWork unitOfWork,
    IAccountsRepository accountsRepository,
    ICategoriesRepository categoriesRepository,
    ITransactionsRepository transactionsRepository
    )
{
    public async Task HandleAsync(Uri baseUri, string token, CancellationToken cancellationToken = default)
    {
        var client = fireflyIiiClientFactory.CreateClient(baseUri, token);

        var (
            fireflyIiiAccounts,
            fireflyIiiTransactions,
            fireflyIiiCategories
        ) = await RetrieveFireflyIiiDataAsync(client, cancellationToken);

        await unitOfWork.WithTransactionAsync(async () =>
        {
            var accounts = await accountsRepository.CreateRangeAsync(
                MakeAccounts(fireflyIiiAccounts), cancellationToken);
            var categories = await categoriesRepository.CreateRangeAsync(
                MakeCategories(fireflyIiiCategories), cancellationToken);
            await transactionsRepository.CreateRangeAsync(
                MakeTransactions(fireflyIiiTransactions, accounts, categories), cancellationToken);
        }, cancellationToken);
    }

    private static IReadOnlyCollection<Account> MakeAccounts(IReadOnlyCollection<AccountRead> fireflyIiiAccounts) =>
        fireflyIiiAccounts
            .DistinctBy(accountRead => accountRead.Attributes.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(accountRead => new Account { Name = accountRead.Attributes.Name })
            .ToList();

    private static IReadOnlyCollection<Category> MakeCategories(IReadOnlyCollection<CategoryRead> fireflyIiiCategories) =>
        fireflyIiiCategories
            .DistinctBy(categoryRead => categoryRead.Attributes.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(categoryRead => new Category
            {
                Name = categoryRead.Attributes.Name,
                Emoji = null
            })
            .ToList();

    private static IReadOnlyCollection<Transaction> MakeTransactions(
        IReadOnlyCollection<TransactionRead> fireflyIiiTransactions,
        IReadOnlyCollection<Account> accounts,
        IReadOnlyCollection<Category> categories
    )
    {
        var accountsByNames = accounts.ToDictionary(
            account => account.Name,
            account => account, StringComparer.InvariantCultureIgnoreCase);
        var categoriesByNames = categories.ToDictionary(
            category => category.Name,
            category => category, StringComparer.InvariantCultureIgnoreCase);
        return fireflyIiiTransactions
            .SelectMany(transactionRead => transactionRead.Attributes.Transactions)
            .OrderByDescending(transaction => transaction.Date)
            .Select(transaction => new Transaction
            {
                Amount = (long) (decimal.Parse(transaction.Amount) * 100),
                Description = transaction.Description,
                ValueDate = transaction.Date,
                SourceAccount = accountsByNames[transaction.Source_name],
                DestinationAccount = accountsByNames[transaction.Destination_name],
                Category = transaction.Category_name is null
                    ? null
                    : categoriesByNames[transaction.Category_name]
            })
            .ToList();
    }

    private static async
        Task<(IReadOnlyCollection<AccountRead> AccountReads, IReadOnlyCollection<TransactionRead> TransactionReads,
            IReadOnlyCollection<CategoryRead> CategoryReads)> RetrieveFireflyIiiDataAsync(FireflyIiiClient client,
            CancellationToken cancellationToken = default)
    {
        var accountReadsTask = client.ListAccountUnpaginatedAsync(cancellationToken: cancellationToken);
        var transactionReadsTask = client.ListTransactionUnpaginatedAsync(cancellationToken: cancellationToken);
        var categoryReadsTask = client.ListCategoryUnpaginatedAsync(cancellationToken: cancellationToken);
        await Task.WhenAll(accountReadsTask, transactionReadsTask, categoryReadsTask);
        return (
            await accountReadsTask,
            await transactionReadsTask,
            await categoryReadsTask
        );
    }
}