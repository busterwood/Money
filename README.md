For .NET 4.6 and .NET Standard 1.3

# Money

The `Money` struct prevents you accidently adding numbers together that repesents *different currencies*.

The `Money` constructor requires a `decimal` amount and an [ISO currency code](https://en.wikipedia.org/wiki/ISO_4217) `string`.

Operators include:

* `+` for addition
* `-` for substraction
* `*` for multiplication
* `/` for division
* `==` for equality
* `!=` for inequality
* `>` for greater than comparision
* `<` for less than comparison
* `-` for negation

There also extension methods add to `decimal` for conversion to some common currencies, i.e. `1m.GBP()`, `2m.USD()` and `3m.EUR()`

## MoneyBag

The `MoneyBag` class is a collection of money in one or more currencies, and has the following methods:

* `Add`
* `Substract`
* `Contains(string currency)`
* `Money Remove(string currency)`
