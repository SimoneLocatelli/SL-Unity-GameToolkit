using System;
using UnityEngine;

/// <summary>
/// This attribute can only be applied to fields because its
/// associated PropertyDrawer only operates on fields (either
/// public or tagged with the [SerializeField] attribute) in
/// the target MonoBehaviour.
/// </summary>
[AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
    public static float SingleCharSize = 10;
    public static float Padding = 20;
    public static float MinSize = 300;

    public readonly string ButtonLabel;
    public readonly string MethodName;

    public InspectorButtonAttribute(string methodName, string buttonLabel)
    {
        CustomAssert.IsNotNullOrWhitespace(methodName, nameof(methodName));
        buttonLabel = string.IsNullOrWhiteSpace(buttonLabel) ? methodName : buttonLabel;
        MethodName = methodName;
        ButtonLabel = buttonLabel;
        ButtonWidth = Math.Max(MinSize, ButtonLabel.Length * SingleCharSize + Padding);
    }

    public float ButtonWidth { get; set; }
}