using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
public class MinMaxRangeAttributeDrawer : PropertyDrawer
{
    private static readonly string[] SupportedTypes = new[]
    {
        typeof(Vector2),
        typeof(Vector2Int),
    }
    .Select(t => t.Name)
    .ToArray();

    private float max = float.MaxValue;
    private float min = float.MinValue;
    private MinMaxRangeAttribute RangeAttribute => (MinMaxRangeAttribute)attribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ValidateType(property);
        Assert.IsNotNull(RangeAttribute);

        // Log about incorrect configuration
        if (RangeAttribute.min > RangeAttribute.max)
            Debug.LogError($"{nameof(MinMaxRangeAttributeDrawer)} - Property '{label}' - Min limit is greater than max limit");
        else if (RangeAttribute.min > RangeAttribute.max)
            Debug.LogWarning($"{nameof(MinMaxRangeAttributeDrawer)} - Property '{label}' - Min limit is equal to max limit");

        // Get Current Values
        var vectorValue = GetCurrentValue(property);
        min = vectorValue.x;
        max = vectorValue.y;

        // Draw property label
        var labelPosition = new Rect(position.x, position.y, position.width, position.height);
        position = EditorGUI.PrefixLabel(labelPosition, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate positions of the various controls
        var controlsRects = GetControlsRects(position);

        // Draw controls - part 1
        // EditorGUI.LabelField(posLabelMin, min.ToString());
        EditorGUI.BeginChangeCheck();

        EditorGUI.FloatField(controlsRects[0], float.Parse(min.ToString("F2")));
        EditorGUI.MinMaxSlider(controlsRects[1], ref min, ref max, RangeAttribute.min, RangeAttribute.max);
        EditorGUI.FloatField(controlsRects[2], float.Parse(max.ToString("F2")));

        // Ensure values are in correct format (rounded and between valid ranges)
        min = Mathf.Clamp(Mathf.Round(min * 100) / 100, RangeAttribute.min, RangeAttribute.max);
        max = Mathf.Clamp(Mathf.Round(max * 100) / 100, RangeAttribute.min, RangeAttribute.max);

        // Update field on serialised object
        var fieldName = property.name;

        if (EditorGUI.EndChangeCheck())
        {
            vectorValue.x = min;
            vectorValue.y = max;
            SetCurrentValue(property, vectorValue);
        }
    }

    private static Rect[] GetControlsRects(Rect position)
    {
        var unitWidthSize = position.width / 10;
        var x = position.x;
        var y = position.y;
        var height = position.height;
        var offset = 10;

        // Debug.Log("unitWidthSize: " + unitWidthSize);

        var posLblMin = new Rect(x, y, unitWidthSize, height);
        var posSlider = new Rect(x + unitWidthSize + offset, y, unitWidthSize * 8 - offset * 2, height);
        var posLblMax = new Rect(x + unitWidthSize * 9, y, unitWidthSize, height);

        var rects = new Rect[3]{
          posLblMin,
          posSlider,
          posLblMax
        };

        return rects;
    }

    private static Vector2 GetCurrentValue(SerializedProperty property)
        => property.type == SupportedTypes[0] ? property.vector2Value : property.vector2IntValue;

    private static void SetCurrentValue(SerializedProperty property, Vector2 vectorValue)
    {
        if (property.type == SupportedTypes[0])
            property.vector2Value = vectorValue;
        else
            property.vector2IntValue = vectorValue.ToVector2Int();
    }

    [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
    private void ValidateType(SerializedProperty property)
    {
        var isSupportedType = SupportedTypes.Contains(property.type);
        if (isSupportedType)
            return; // return as we don't want to create the assert message every single time, even in debug mode;

        Assert.IsTrue(isSupportedType, $"Property '{property.name}' must be one of the supported types '{string.Join(',', SupportedTypes)}', but actual type is '{property.type}'");
    }
}