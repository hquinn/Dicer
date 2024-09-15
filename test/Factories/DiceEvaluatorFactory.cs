using Dicer.Tests.Helpers;

namespace Dicer.Tests.Factories;

public static class DiceEvaluatorFactory
{
    public static IDiceEvaluator CreateWithDefaults()
    {
        return new DiceEvaluator();
    }

    public static IDiceEvaluator Create()
    {
        return new DiceEvaluator(new SequentialRandom());
    }
}