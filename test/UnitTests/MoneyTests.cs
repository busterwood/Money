using BusterWood.Monies;
using NUnit.Framework;
using System;

namespace UnitTests
{
    [TestFixture]
    public class MoneyTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("   ")]
        [TestCase("A")]
        [TestCase("AB")]
        [TestCase("ABCC")]
        public void cannot_create_money_with_invalid_isocode(string isoCode)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Money(1m, isoCode));
        }

        [Test]
        public void can_add_money_with_same_currency()
        {
            var result = new Money(1, "GBP") + new Money(1, "GBP");
            Assert.AreEqual(2m, result.Amount, "Amount");
            Assert.AreEqual("GBP", result.Currency, "IsoCode");
        }

        [Test]
        public void cannot_add_money_with_different_currency()
        {
            Assert.Throws<InvalidOperationException>(() => { var result = new Money(1, "GBP") + new Money(1, "EUR"); });
        }

        [Test]
        public void can_subtract_money_with_same_currency()
        {
            var result = new Money(2, "GBP") - new Money(1, "GBP");
            Assert.AreEqual(1m, result.Amount, "Amount");
            Assert.AreEqual("GBP", result.Currency, "IsoCode");
        }

        [Test]
        public void cannot_subtract_money_with_different_currency()
        {
            Assert.Throws<InvalidOperationException>(() => { var result = new Money(1, "GBP") - new Money(1, "EUR"); });
        }

        [Test]
        public void can_multiply_by_a_number()
        {
            var result = new Money(2, "GBP") * 2;
            Assert.AreEqual(4m, result.Amount, "Amount");
            Assert.AreEqual("GBP", result.Currency, "IsoCode");
        }

        [Test]
        public void can_divide_by_a_number()
        {
            var result = new Money(4, "GBP") / 2;
            Assert.AreEqual(2m, result.Amount, "Amount");
            Assert.AreEqual("GBP", result.Currency, "IsoCode");
        }

        [Test]
        public void can_be_equal_with_same_amount_and_currency()
        {
            Assert.AreEqual(new Money(2, "GBP"), new Money(2, "GBP"));
        }

        [Test]
        public void not_equal_with_same_amount_and_differnet_currency()
        {
            Assert.AreNotEqual(new Money(2, "GBP"), new Money(1, "USD"));
        }

        [Test]
        public void not_equal_with_differnet_amount_and_same_currency()
        {
            Assert.AreNotEqual(new Money(1.00m, "GBP"), new Money(1.01m, "GBP"));
        }

        [Test]
        public void can_be_equal_with_operator_with_same_amount_and_currency()
        {
            Assert.IsTrue(new Money(2, "GBP") == new Money(2, "GBP"));
        }

        [Test]
        public void not_equal_with_operator_with_same_amount_and_differnet_currency()
        {
            Assert.IsFalse(new Money(2, "GBP") == new Money(1, "USD"));
        }

        [Test]
        public void not_equal_with_operator_with_differnet_amount_and_same_currency()
        {
            Assert.IsFalse(new Money(1.00m, "GBP") == new Money(1.01m, "GBP"));
        }

        [Test]
        public void can_be_not_equal_with_operator2_with_same_amount_and_currency()
        {
            Assert.IsFalse(new Money(2, "GBP") != new Money(2, "GBP"));
        }

        [Test]
        public void not_equal_with_operator2_with_same_amount_and_differnet_currency()
        {
            Assert.IsTrue(new Money(2, "GBP") != new Money(1, "USD"));
        }

        [Test]
        public void not_equal_with_operator2_with_differnet_amount_and_same_currency()
        {
            Assert.IsTrue(new Money(1.00m, "GBP") != new Money(1.01m, "GBP"));
        }

        [Test]
        public void compare_returns_zero_when_money_is_equal()
        {
            Assert.AreEqual(0, 10m.GBP().CompareTo(10m.GBP()));
        }

        [Test]
        public void compare_returns_minus_one_when_left_value_is_less_than_right_value()
        {
            Assert.AreEqual(-1, 9m.GBP().CompareTo(10m.GBP()));
        }

        [Test]
        public void compare_returns_plus_one_when_left_value_is_more_than_right_value()
        {
            Assert.AreEqual(1, 11m.GBP().CompareTo(10m.GBP()));
        }

        [Test]
        public void can_negate_positive_money()
        {
            var m = 1m.GBP();
            Assert.AreEqual(-1m, -m.Amount);
        }

        [Test]
        public void can_negate_negative_money()
        {
            var m = -1m.GBP();
            Assert.AreEqual(1m, -m.Amount);
        }
    }
}
