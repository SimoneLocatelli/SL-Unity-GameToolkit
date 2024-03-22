using System.Diagnostics;
using UnityEngine;

public class DebugStringDrawer
{
    [Conditional("DEBUG")]
    public static void DrawString(string text, Vector3 worldPos, float oX = 0, float oY = 0, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();

        var view = UnityEditor.SceneView.currentDrawingSceneView;

        if (view == null)
        {
            UnityEditor.Handles.EndGUI();
            return;
        }

        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

        if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
        {
            UnityEditor.Handles.EndGUI();
            return;
        }
        var style = new GUIStyle
        {
            fontSize = 12,
            fontStyle = FontStyle.Bold
        };

        if (colour.HasValue)
            style.normal.textColor = colour.Value;

        UnityEditor.Handles.Label(TransformByPixel(worldPos, oX, oY), text, style);

        UnityEditor.Handles.EndGUI();
    }

    private static Vector3 TransformByPixel(Vector3 position, float x, float y)
        => TransformByPixel(position, new Vector3(x, y));

    private static Vector3 TransformByPixel(Vector3 position, Vector3 translateBy)
    {
        Camera cam = UnityEditor.SceneView.currentDrawingSceneView.camera;
        if (cam)
            return cam.ScreenToWorldPoint(cam.WorldToScreenPoint(position) + translateBy);

        return position;
    }
}