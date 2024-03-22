using UnityEngine;

public static class Chance
{
    /// <summary>
    /// Enter a value between 0 - 100 to calculate
    /// if succesfull
    /// </summary>
    /// <param name="successPercentage"></param>
    public static bool Calculate(int successPercentage)
    {
        if (successPercentage <= 0)
            return false;
        if (successPercentage >= 100)
            return true;

        var perc = Random.Range(1, 100f); // max exclusive
        return perc <= successPercentage;
    }

    internal static bool FiftyFifty()
    {
        var result = UnityEngine.Random.Range(0, 2) == 1;

        return result;
    }
}