using System;

public static class TimeSpanExtensions
{
    public static bool IsBetween(this TimeSpan ts, int fromHour, int toHour)
    {
        if (fromHour <= toHour)
            return ts.Hours >= fromHour && ts.Hours <= toHour;

        return !(ts.Hours >= toHour && ts.Hours < fromHour);
    }
}