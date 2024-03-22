using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BasePropertyDrawer : PropertyDrawer, ICustomEditor
{
    #region Properties

    protected virtual bool HideLabel { get => false; }

    protected int IndentLevel
    {
        get => EditorGUI.indentLevel;
        set => EditorGUI.indentLevel = Mathf.Max(0, value);
    }

    protected GUIContent Label { get; private set; }
    protected Rect Position { get; private set; }
    protected SerializedProperty Property { get; private set; }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        => HideLabel ? 0 : base.GetPropertyHeight(property, label);

    #endregion Properties

    #region Methods

    public SerializedProperty FindProperty(string propertyName)
        => Property.FindPropertyRelative(propertyName);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Property = property;
        Position = position;
        Label = label;

        EditorGUI.BeginProperty(position, GUIContent.none, property);

        OnDrawProperty();

        EditorGUI.EndProperty();
    }

    protected TEnum DrawEnumProperty<TEnum>(string propertyName) where TEnum : Enum
        => CustomEditorHelper.DrawEnumProperty<TEnum>(this, propertyName);

    protected void DrawHeader(string header)
            => CustomEditorHelper.DrawHeader(header);

    protected void DrawProperty(string propertyName)
        => CustomEditorHelper.DrawProperty(this, propertyName);

    //protected void GetTargetObject<T>() where T : UnityEngine.Object
    //{
    //    var t = Property.serializedObject.targetObject as T;
    //}

    protected Rect HidePropertyLabel()
                    => EditorGUI.PrefixLabel(new Rect(Position.position, new Vector2(0, 0)), GUIUtility.GetControlID(FocusType.Passive), Label);

    protected abstract void OnDrawProperty();

    protected void Space(int lines = 1)
        => CustomEditorHelper.Space(lines);

    #endregion Methods
}