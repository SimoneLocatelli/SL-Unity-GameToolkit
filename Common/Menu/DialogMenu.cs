using UnityEngine;

public static class DialogMenu
{
    private static IMenuInputReceiver MenuInputReceiver;
    public static bool IsActive { get => MenuInputReceiver != null; }

    public static void CloseMenu()
    {
        MenuInputReceiver.Close();
        MenuInputReceiver = null;
    }

    public static void OnCancel() => MenuInputReceiver?.OnCancel();

    public static void OnConfirm() => MenuInputReceiver?.OnConfirm();

    public static void OnMovement(Vector2 vector2) => MenuInputReceiver?.OnMovement(vector2);

    public static void OpenMenu(IMenuInputReceiver menuInputReceiver)
    {
        MenuInputReceiver = menuInputReceiver;
        menuInputReceiver.Open();
    }
}