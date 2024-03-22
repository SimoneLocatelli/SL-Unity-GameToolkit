using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(BaseInteractableBehaviour), true)]
internal class InteractableBehaviourEditor : BaseEditor<BaseInteractableBehaviour>
{
    protected override void OnDrawProperties()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Activate Icon"))
            Target.AddActivateIcon();
    }
}