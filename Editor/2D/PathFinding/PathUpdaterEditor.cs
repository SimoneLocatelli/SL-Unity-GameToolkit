using UnityEditor;

[CustomEditor(typeof(PathUpdater))]
public class PathUpdaterEditor : BaseEditor<PathUpdater>
{
    protected override void OnDrawProperties()
    {
        DrawProperty(PathUpdater.Node2DGridPropertyName);

        DrawProperty(nameof(PathUpdater.CanSearch));

        DrawHeader("Target");
        DrawProperty(nameof(PathUpdater.PathTarget));
        DrawProperty(PathUpdater.Collider2DPivotPointFieldName);

        DrawHeader("Refresh");
        DrawProperty(nameof(PathUpdater.RefreshInterval));
        DrawProperty(nameof(PathUpdater.RefreshMode));
    }
}