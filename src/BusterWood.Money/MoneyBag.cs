using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusterWood.Monies
{
    /// <summary>
    /// A container for money of one or more currencies, i.e. 10 GBP and 20 USD
    /// </summary>
    public class MoneyBag : IEnumerable<Money>
    {
        readonly Dictionary<string, decimal> _contents = new Dictionary<string, decimal>();

        public Money this[string currency]
        {
            get
            {
                Money.CheckIsoCode(currency);
                decimal amount;
                _contents.TryGetValue(currency, out amount);
                return new Money(amount, currency);
            }
        }

        public void Add(Money money)
        {
            Money.CheckIsoCode(money.Currency);
            decimal amount;
            _contents.TryGetValue(money.Currency, out amount);
            _contents[money.Currency] = amount + money.Amount;
        }

        public bool Contains(string currency) => _contents.ContainsKey(currency);

        public IEnumerator<Money> GetEnumerator()
        {
            foreach (var pair in _contents)
            {
                yield return new Money(pair.Value, pair.Key);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsEmpty
        {
            get
            {
                if (_contents.Count == 0)
                    return true;
                foreach (var amount in _contents.Values)
                {
                    if (amount != 0m) return false;
                }
                return true;
            }
        }

        public Money Remove(string currency)
        {
            Money.CheckIsoCode(currency);
            decimal amount;
            if (_contents.TryGetValue(currency, out amount))
            {
                _contents.Remove(currency);
                return new Money(amount, currency);
            }
            return new Money(0m, currency);
        }

        public void Subtract(Money money) => Add(-money);

        public override string ToString()
        {
            var sb = new StringBuilder(10);
            sb.Append("[ ");
            foreach (var pair in _contents)
            {
                sb.AppendFormat("{0:N2} {1}, ", pair.Value, pair.Key);
            }
            if (_contents.Count > 0)
                sb.Length -= 2; // remove last comma and space
            sb.Append(" ]");
            return sb.ToString();
        }
    }
}