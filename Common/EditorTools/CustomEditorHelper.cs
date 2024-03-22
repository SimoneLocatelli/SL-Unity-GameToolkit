using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public static class CustomEditorHelper

{
    public const float DefaultPropertyHeight = 18;

    public static void DrawButton(string header, Action onButtonPressed)
    {
        if (GUILayout.Button(header))
            onButtonPressed();
    }

    public static TEnum DrawEnumProperty<TEnum>(ICustomEditor customEditor, string propertyName) where TEnum : Enum
    {
        var property = customEditor.FindProperty(propertyName);
        TEnum currentValue = (TEnum)(object)property.intValue;
        currentValue = (TEnum)EditorGUILayout.EnumPopup(ObjectNames.NicifyVariableName(propertyName), currentValue);
        property.UpdateEnumProperty(currentValue);

        return currentValue;
    }

    public static void DrawHeader(string header)
        => EditorGUILayout.LabelField(header, EditorStyles.boldLabel);

    public static void DrawSeparator()
        => EditorGUILayout.Separator();

    public static bool DrawProperty(ICustomEditor customEditor, string propertyName)
    {
        var property = customEditor.FindProperty(propertyName);

        Assert.IsNotNull(property, $"Couldn't find a property named {propertyName}");

        return DrawProperty(property);
    }

    public static bool DrawProperty(SerializedProperty property)
        => EditorGUILayout.PropertyField(property);

    public static void Space(int lines = 1)
    {
        CustomAssert.IsGreaterThan(lines, 0, nameof(lines));

        GUILayout.Space(DefaultPropertyHeight * lines);
    }
}