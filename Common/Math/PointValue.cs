public class PointValue<T>
{
    public readonly int X;

    public PointValue(int x, int y, T value)
    {
        X = x;
        Y = y;
        Value = value;
    }

    public readonly int Y;
    public readonly T Value;
}