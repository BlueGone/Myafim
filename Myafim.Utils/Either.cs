using System.Diagnostics.CodeAnalysis;

namespace Myafim.Utils;

public abstract record Either<TLeft, TRight>
{
    private Either() {}
    
    public static implicit operator Either<TLeft, TRight>(TLeft left) => new Left(left);
    public static implicit operator Either<TLeft, TRight>(TRight right) => new Right(right);

    public abstract bool TryGetRight([MaybeNullWhen(false)] out TRight right);
    public abstract bool TryGetRight(
        [MaybeNullWhen(false)] out TRight right,
        [MaybeNullWhen(true)] out TLeft left);

    public sealed record Right(TRight Value) : Either<TLeft, TRight>
    {
        public override bool TryGetRight([MaybeNullWhen(false)] out TRight right)
        {
            right = Value;
            return true;
        }
        
        public override bool TryGetRight(
            [MaybeNullWhen(false)] out TRight right,
            [MaybeNullWhen(true)] out TLeft left)
        {
            right = Value;
            left = default;
            return true;
        }
    }

    public sealed record Left(TLeft Value) : Either<TLeft, TRight>
    {
        public override bool TryGetRight([MaybeNullWhen(false)] out TRight right)
        {
            right = default;
            return false;
        }

        public override bool TryGetRight(
            [MaybeNullWhen(false)] out TRight right,
            [MaybeNullWhen(true)] out TLeft left)
        {
            right = default;
            left = Value;
            return false;
        }
    }
}