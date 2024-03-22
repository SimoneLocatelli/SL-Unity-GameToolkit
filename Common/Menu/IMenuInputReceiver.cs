using UnityEngine;

public interface IMenuInputReceiver
{
    void OnMovement(Vector2 vector2);

    void OnCancel();

    void OnConfirm();

    void Open();

    void Close();
}