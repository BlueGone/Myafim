namespace Myafim.FireflyIii.Client;

public partial class FireflyIiiClient
{
    public Task<IReadOnlyCollection<AccountRead>> ListAccountUnpaginatedAsync(Guid? x_Trace_Id = null, DateTimeOffset? date = null, AccountTypeFilter? type = null) =>
        UnpaginateAsync<AccountRead>(async page =>
        {
            var response =
                await ListAccountAsync(x_Trace_Id: x_Trace_Id, limit: null, page: page, date: date, type: type);

            return (page < response.Meta.Pagination.Total_pages, response.Data);
        });
    
    public Task<IReadOnlyCollection<TransactionRead>> ListTransactionUnpaginatedAsync(Guid? x_Trace_Id = null, DateTimeOffset? start = null, DateTimeOffset? end = null, TransactionTypeFilter? type = null) =>
        UnpaginateAsync<TransactionRead>(async page =>
        {
            var response =
                await ListTransactionAsync(x_Trace_Id: x_Trace_Id, limit: null, page: page, start: start, end: end, type: type);

            return (page < response.Meta.Pagination.Total_pages, response.Data);
        });

    public Task<IReadOnlyCollection<CategoryRead>> ListCategoryUnpaginatedAsync(Guid? x_Trace_Id = null) =>
        UnpaginateAsync<CategoryRead>(async page =>
        {
            var response = await ListCategoryAsync(x_Trace_Id: x_Trace_Id, limit: null, page: page);

            return (page < response.Meta.Pagination.Total_pages, response.Data);
        });
    

    private static async Task<IReadOnlyCollection<T>> UnpaginateAsync<T>
    (
        Func<int, Task<(bool HasNextPage, IEnumerable<T> Page)>> getNextPageAsync
    )
    {
        bool hasNextPage = true;
        int page = 1;
        
        var list = new List<T>();
        
        while (hasNextPage)
        {
            (hasNextPage, var pageCollection) = await getNextPageAsync(page);
            list.AddRange(pageCollection);
            page++;
        }

        return list;
    }
}