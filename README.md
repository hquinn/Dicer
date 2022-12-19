# Dicer - An open source dice expression library

![NuGet](https://img.shields.io/nuget/v/Dicer)
![NuGet](https://img.shields.io/nuget/dt/Dicer)

Dicer is a simple library which enables parsing of dice expressions for your application.

### How to use
#### Basic usage
To use Dicer directly, you can include Dicer into your classes by declaring a static using like:
```csharp
using static Dicer.Parser;
```

And you can `Parse` and `Evaluate` expresions like:
```csharp
NodeResponse response = Parse("1d8 + 3").Evaluate();
```

#### NodeResponse deep-dive
Once an expression has been evaluated, the return type is a `NodeResponse`. `NodeResponse` has a `Result` property which represents the final calculated value after `Evaluate`, and `RollResponses` which displays all the rolls that were rolled in the expression. 

`RollResponses` is an enumerable of `RollResponse` which contains a `Result` for the roll, `Rolls` for all the rolls in the response that were "kept", and `Discarded` for all the rolls in the response that were "discarded" (more on this later). 

Both `Rolls` and `Discarded` are an enumerable of `Roll` which represents a single die roll and has the `Result` property, and the `DieSize`.

#### Expressions
Dicer has full BOMDAS (a.k.a PEMDAS) support (dice expressions has a higher precedence than multiplication/division) and partial support for dice notations standard. The features that it includes are:

 - Arithmetic: `4 + 3 / 2 * 1`
 - Unary: `1 - -1`
 - Bracket: `(4 + 3) / 2 * 1`
 - Dice Expression: `1d8 + 3`
   - Keep Higest: `4d6k3` -> Leftover dice will be populated under `Discarded`
   - Keep Lowest: `2d20k-1` -> Leftover dice will be populated under `Discarded`
   - Minimum Roll: `1d6m2`
   
All of these are fully supported together, and can create complex expressions like `4D(5/(1+2))K-3 +- (5*(1+2))d10m(1d2k1)`.

#### Evaluate
The `Evaluate` method is defaulted to work out of the box for random dice rolling, and the `Result` being rounded down (floor). 

Dicer does support non-random rolling via `Min` (will roll 1 for all dice), `Average` (will roll the fixed value average for all dice), and `Max` (will roll the die size for all dice).

Dicer also supports rounding strategies, such as `RoundToCeiling`, `RoundToFloor`, `RoundToNearest`, and `NoRounding`. Note that dice expressions will always be rounded before other math expressions regardless if `NoRounding` is selected (e.g. `1d8 + (5 / 2)` will round the result of `1d8` first).

To use, simply pass the overloaded parameters into the `Evaluate` function:

```csharp
NodeResponse response = Parse("1d8 + (5 / 2)").Evaluate(Roller.Max, RoundingStrategy.NoRounding);
```

#### Repeating Dice
Dicer has support for repeating dice expressions as an overload for `Parse`:

```csharp
IEnumerable<NodeResponse> responses = Parse("4D6K3", "1d6").Evaluate();
```

In the above example, `1d6` will be evaluated first to find out how many times to evaluate `4D6K3`. All of the results will be filled out in the `NodeResponse` enumerable result for use.

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
 - Minimum and Maximum reroll limit (i.e. if `1d6m1` evaluates to 1 the first time and upon reroll it still evaluates to 1, keep value)
 - Roll history (to support the Minimum and Maximum roll values)
 - Exploding dice
 - Percentile dice (will need more consideration)
 
If you would like to implement any of the above features, you're more than happy to make a pull request. If you have any additional ideas, please create an issue.