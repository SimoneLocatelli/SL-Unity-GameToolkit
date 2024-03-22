using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Assertions;

public static class CustomAssert
{
    [Conditional("UNITY_ASSERTIONS")]
    public static void AssertNoDuplicates<T>(IEnumerable<T> collection, string variableName)
    {
        IsNotNull(collection, variableName);

        var count = collection.Count();
        var newCount = collection.Distinct().ToList().Count;

        IsTrue(count == newCount, $"'{variableName}' contains duplicate values found");
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void CountIsLessThan<TSource>(List<TSource> list, int max)
        => IsTrue(list.Count <= max, $"Too many items in the list ({list.Count}), max supported is 3");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsEnumDefined<T>(T value) where T : Enum
        => IsTrue(Enum.IsDefined(typeof(T), value), $"Value {value} does not belong to enum {typeof(T).FullName}");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsEnumDefined<T>(object value) where T : Enum
        => IsTrue(Enum.IsDefined(typeof(T), value), $"Value {value} does not belong to enum {typeof(T).FullName}");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsFalse(bool condition, string variableName)
        => Assert.IsFalse(condition, variableName);

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsGreaterThan(float value, float minValue, string variableName, string variableName2 = null)
    {
        if (string.IsNullOrWhiteSpace(variableName2))
            IsTrue(value > minValue, $"'{variableName}' with value '{value} must be greater than value {minValue}'");
        else
            IsTrue(value > minValue, $"'{variableName}' with value '{value} must be greater than variable '{variableName2}' with value {minValue}'");
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsInRange(int value, int max, int min = 0, bool includeExtremes = true)
    {
        if (includeExtremes)
            IsTrue(value >= min && value <= max, $"Value must be between {min} and {max} (Extremes included) but was equal to {value}");
        else
            IsTrue(value > min && value < max, $"Value must be between {min} and {max} (Extremes excluded) but was equal to {value}");
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotEmpty<T>(IEnumerable<T> collection, string variableName)
    {
        IsNotNull(collection, variableName);
        Assert.IsTrue(collection.Any(), $"'{variableName}' cannot be empty");
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotEqual<TSource>(TSource value, TSource valueToNotBeEqualTo, string variableName1, string variableName2)
        => IsFalse(object.Equals(value, valueToNotBeEqualTo), $"'{variableName1}' and '{variableName2}' cannot be equal");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNegative(int value, string variableName)
        => IsTrue(value >= 0, $"'{variableName}' cannot be a negative number");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNegative(float value, string variableName)
        => IsTrue(value >= 0, $"'{variableName}' cannot be a negative number");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNull(object obj, string variableName)
        => Assert.IsNotNull(obj, $"'{variableName}' cannot be null");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNullOrWhitespace(string value, string variableName)
        => IsFalse(string.IsNullOrWhiteSpace(value), $"'{variableName} cannot be null or whitespace");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsTrue(bool condition, string message)
        => Assert.IsTrue(condition, message);

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsType<T>(object obj, string variableName)
        => IsTrue(obj is T, $"'{variableName} must be of type {typeof(T).FullName}");

    [Conditional("UNITY_ASSERTIONS")]
    public static void IsType(object obj, string variableName, Type type)
    {
        IsNotNull(obj, variableName);
        IsNotNull(type, nameof(type));
        IsTrue(obj.GetType() == type, $"'{variableName} must be of type {type.FullName}");
    }
}