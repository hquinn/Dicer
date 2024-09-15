# Dicer - An open source dice expression library

![NuGet](https://img.shields.io/nuget/v/Dicer)
![NuGet](https://img.shields.io/nuget/dt/Dicer)

Dicer is a simple library which enables parsing of dice expressions for your application.

### How to use

#### Usage

To use Dicer directly, you can include Dicer into your classes including the Dicer namespace:

```csharp
using Dicer;
```

Afterwards, you can create an instance of `IDiceEvaluator`:

```csharp
IDiceEvaluator diceEvaluator = new DiceEvaluator();
```

**Note: `DiceEvaluator` should be used as a singleton, as it contains the random number generator as well as it
initializes a number of objects which should be re-used. If you're not using a DI Container, this can be achieved by:**

```csharp
IDiceEvaluator diceEvaluator = DiceEvaluator.Instance;
```

You can then create an `IDiceExpression` by using the `DiceExpressionParser.Parse` static method:

```csharp
IDiceExpression expression = DiceExpressionParser.Parse("1d8 + 3");
```

Finally, you can evaluate the expression by calling the `Evaluate` method on the `IDiceEvaluator`:

```csharp
ExpressionResponse response = diceEvaluator.Evaluate(expression);
```

#### ExpressionResponse deep-dive

Once an expression has been evaluated, the return type is an `ExpressionResponse`. `ExpressionResponse` has a `Result`
property which represents the final calculated value after evaluation, and `RollResponses` which displays all the rolls
that were rolled in the expression.

`RollResponses` is a collection of `RollResponse` which contains a `Result` for the roll, `Rolls` for all the rolls in
the response that were "kept", and `Discarded` for all the rolls in the response that were "discarded" (more on this
later).

Both `Rolls` and `Discarded` are a collection of `Roll` which represents a single die roll and has the `Result`
property, and the `DieSize`.

#### Expressions

Dicer has full BOMDAS (a.k.a PEMDAS) support (dice expressions has a higher precedence than multiplication/division) and
partial support for dice notations standard. The features that it includes are:

- Arithmetic: `4 + 3 / 2 * 1`
- Unary: `1 - -1`
- Bracket: `(4 + 3) / 2 * 1`
- Dice Expression: `1d8 + 3`
    - Keep Higest: `4d6k3` -> Leftover dice will be populated under `Discarded`
    - Keep Lowest: `2d20k-1` -> Leftover dice will be populated under `Discarded`
    - Minimum Roll: `1d6m2`

All of these are fully supported together, and can create complex expressions like
`4D(5/(1+2))K-3 +- (5*(1+2))d10m(1d2k1)`.

#### Evaluate

The `Evaluate` method on `IDiceEvaluator` is defaulted to work out of the box for random dice rolling, and the `Result`
being rounded down (floor).

Dicer does support non-random rolling via `Min` (will roll 1 for all dice), `Average` (will roll the fixed value average
for all dice), and `Max` (will roll the die size for all dice).

Dicer also supports rounding strategies (`RoundingStrategy`), such as `RoundToCeiling`, `RoundToFloor` (default),
`RoundToNearest`, and `NoRounding`, which will round the final value returned. Since dice expressions can only be
represented with discrete numbers (doesn't make sense for dice to be represented with continuous numbers), there's an
additional overload to specifically round dice values (`DiceRoundingStrategy`) with the possible values being
`RoundToCeiling` (default), `RoundToFloor`, and `RoundToNearest`.

To use, simply pass the overloaded parameters into the `Evaluate` function:

```csharp
IDiceExpression expression = DiceExpressionParser.Parse("1d8 + 3");
ExpressionResponse response = diceEvaluator.Evaluate(expression, Roller.Max, RoundingStrategy.NoRounding, DiceRoundingStrategy.RoundToNearest);
```

#### Repeating Dice

Dicer has support for repeating dice expressions as an overload for `Evaluate`:

```csharp
IDiceExpression expression = DiceExpressionParser.Parse("4D6K3");
IReadOnlyCollection<ExpressionResponse> responses = diceEvaluator.Evaluate(expression, 6);

// OR
IDiceExpression expression = DiceExpressionParser.Parse("4D6K3");
IDiceExpression repeatingExpression = DiceExpressionParser.Parse("1D6");
IReadOnlyCollection<ExpressionResponse> responses = diceEvaluator.Evaluate(expression, repeatingExpression);
```

In the second example, `1d6` will be evaluated first to find out how many times to evaluate `4D6K3`. All of the results
will be filled out in the `ExpressionResponse` collection type for use.

#### Testing

By default, the `DiceEvaluator` uses an internal `Random` object to generate random numbers. If you would like to test
the `DiceEvaluator` with a fixed seed, you can create your own `IRandom` implementation and pass it into the
`DiceEvaluator`:

```csharp
public class FixedRandom : IRandom
{
    private readonly Random random;

    public FixedRandom(int seed)
    {
        random = new Random(seed);
    }

    public int RollDice(int dieSize)
    {
        return random.Next(1, dieSize + 1);
    }
}

IDiceEvaluator diceEvaluator = new DiceEvaluator(new FixedRandom(1234));
```

### Installation

Install Dicer via NuGet CLI:

```
Install-Package Dicer
```

Or via the .NET CLI

```
dotnet add package Dicer
```

### Plans

I don't have a fixed schedule to work on this project, but will eventually like to work on the following ideas:

- Maximum roll value
- Minimum and Maximum reroll limit (i.e. if `1d6m1` evaluates to 1 the first time and upon reroll it still evaluates to
  1, keep value)
- Roll history (to support the Minimum and Maximum roll values)
- Exploding dice
- Percentile dice (will need more consideration)

If you would like to implement any of the above features, you're more than happy to make a pull request. If you have any
additional ideas, please create an issue.