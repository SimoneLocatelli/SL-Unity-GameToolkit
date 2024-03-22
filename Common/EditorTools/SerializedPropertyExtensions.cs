using System;
using UnityEditor;

public static class SerializedPropertyExtensions
{

    public static void UpdateEnumProperty(this SerializedProperty property, Enum value)
    {
        property.intValue = (int)Convert.ChangeType(value, typeof(int));
    }
}