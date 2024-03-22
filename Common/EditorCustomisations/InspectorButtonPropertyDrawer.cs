#if UNITY_EDITOR
using UnityEngine;

using UnityEditor;

using System.Reflection;
using System;
using UnityEngine.Assertions;

[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        Assert.IsNotNull(attribute, nameof(attribute));
        CustomAssert.IsType(attribute, nameof(attribute), typeof(InspectorButtonAttribute));

        var inspectorButtonAttribute = (InspectorButtonAttribute)attribute;

        var buttonRect = GetButtonRect(position, inspectorButtonAttribute);

        if (!GUI.Button(buttonRect, inspectorButtonAttribute.ButtonLabel))
            return;
        var eventOwnerType = prop.serializedObject.targetObject.GetType();
        var eventName = inspectorButtonAttribute.MethodName;

        _eventMethodInfo ??= GetMethod(eventOwnerType, eventName);

        if (_eventMethodInfo == null)
        {
            Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
            return;
        }

        _eventMethodInfo?.Invoke(prop.serializedObject.targetObject, null);
    }

    private Rect GetButtonRect(Rect position, InspectorButtonAttribute inspectorButtonAttribute)
    {
        var x = position.x + (position.width - inspectorButtonAttribute.ButtonWidth) * 0.5f;
        var width = inspectorButtonAttribute.ButtonWidth;
        var y = position.y;
        var height = position.height;
        return new Rect(x, y, width, height);
    }

    private static MethodInfo GetMethod(Type eventOwnerType, string eventName)
        => eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
}

#endif
