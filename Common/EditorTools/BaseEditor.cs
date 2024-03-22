using System;
using UnityEditor;

public abstract class BaseEditor<T> : Editor, ICustomEditor where T : UnityEngine.Object
{
    public T Target { get => (T)target; }

    public SerializedProperty FindProperty(string propertyName)
        => serializedObject.FindProperty(propertyName);

    public override sealed void OnInspectorGUI()
    {
        serializedObject.Update();

        OnDrawProperties();

        ApplyModifiedProperties();
    }

    protected void DrawButton(string header, Action onButtonPressed)
        => CustomEditorHelper.DrawButton(header, onButtonPressed);

    protected void DrawHeader(string header)
        => CustomEditorHelper.DrawHeader(header);

    protected bool DrawProperty(SerializedProperty property)
        => EditorGUILayout.PropertyField(property);

    protected bool DrawProperty(string propertyName)
        => CustomEditorHelper.DrawProperty(this, propertyName);

    protected void DrawSeparator()
        => CustomEditorHelper.DrawSeparator();

    protected abstract void OnDrawProperties();

    protected void Space(int lines = 1)
        => CustomEditorHelper.Space(lines);

    private void ApplyModifiedProperties()
        => serializedObject.ApplyModifiedProperties();
}