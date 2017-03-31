using System;

namespace BusterWood.Monies
{
    /// <summary>An amount of a specific currency, e.g. 10.99 GBP or 100.01 USD</summary>
    public struct Money : IEquatable<Money>, IComparable<Money>
    {
        public static Money None;

        /// <summary>The amount of money</summary>
        public decimal Amount { get; }

        /// <summary>ISO Code of the currency, e.g. GBP, USD, EUR</summary>
        public string Currency { get; }

        /// <summary>Create a new currency value</summary>
        /// <param name="amount">Amount of money</param>
        /// <param name="currency">ISO Code of the currency, e.g. GBP, USD, EUR</param>
        public Money(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(currency), "Must be 3 character ISO code");
            Amount = amount;
            Currency = currency;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode() ^ (Currency == null ? 0 : Currency.GetHashCode());
        }

        public override bool Equals(object obj) => obj is Money && base.Equals((Money)obj);

        public bool Equals(Money other) => Amount == other.Amount && string.Equals(Currency, other.Currency, StringComparison.Ordinal);

        public override string ToString() => $"{Amount} {Currency}";

        public string ToString(string format) => $"{Amount.ToString(format)} {Currency}";

        public int CompareTo(Money other)
        {
            int result = (Currency?.CompareTo(other.Currency ?? "")).GetValueOrDefault();
            if (result != 0) return result;
            return Amount.CompareTo(other.Amount);
        }

        public static bool operator ==(Money left, Money right) => left.Equals(right);

        public static bool operator !=(Money left, Money right) => !left.Equals(right);

        public static Money operator +(Money left, Money right)
        {
            if (!string.Equals(left.Currency, right.Currency, StringComparison.Ordinal))
                throw new InvalidOperationException($"Cannot add {left.Currency} to {right.Currency}");
            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            if (!string.Equals(left.Currency, right.Currency, StringComparison.Ordinal))
                throw new InvalidOperationException($"Cannot subtract {left.Currency} from {right.Currency}");
            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator *(Money value, decimal times) => new Money(value.Amount * times, value.Currency);

        public static Money operator /(Money value, decimal divisor) => new Money(value.Amount / divisor, value.Currency);
    }

    public static class Extensions
    {
        public static Money GBP(this decimal value) => new Money(value, "GBP");
        public static Money USD(this decimal value) => new Money(value, "USD");
        public static Money EUR(this decimal value) => new Money(value, "EUR");
    }
}
