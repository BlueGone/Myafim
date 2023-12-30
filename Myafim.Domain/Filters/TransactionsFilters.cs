namespace Myafim.Domain.Filters;

public class TransactionsFilters
{
    public DateTimeOffset? MinValueDate { get; set; }
    public DateTimeOffset? MaxValueDate { get; set; }
}