using System;
using UnityEditor;

public static class SerializedPropertyExtensions
{
    public static TEnum ToEnumValue<TEnum>(this SerializedProperty property) where TEnum : Enum
        => (TEnum)Enum.Parse(typeof(TEnum), property.enumNames[property.enumValueIndex]);
}