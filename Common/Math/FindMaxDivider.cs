public static class FindMaxDivider
{
    public static int FindLargestInt(float currentDivider, params float[] values)
    {
        for (var currentDividerInt = (int)currentDivider;
            currentDividerInt > 1;
            currentDividerInt--)
        {
            if (IsDivider(currentDividerInt, values))
                return currentDividerInt;
        }

        return 1;
    }

    public static bool IsDivider(int divider, params float[] values)
    {
        for (int i = 0; i < values.Length; i++)
            if (values[i] % divider != 0) return false;

        return true;
    }
}