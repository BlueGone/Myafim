namespace Myafim.Domain.Errors;

public abstract record TransactionError
{
    protected TransactionError() {}
    public sealed record AmountMustBeStrictlyPositive : TransactionError;
    public sealed record DescriptionMustBeLessThan80Characters : TransactionError;
    public sealed record SourceAndDestinationAccountsMustBeDifferent : TransactionError;
    public sealed record SourceAccountDoesNotExist : TransactionError;
    public sealed record DestinationAccountDoesNotExist : TransactionError;
    public sealed record CategoryDoesNotExist : TransactionError;
}