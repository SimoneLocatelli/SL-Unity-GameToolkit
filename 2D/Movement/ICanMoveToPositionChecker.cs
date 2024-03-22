using System.Collections;
using UnityEngine;

public interface ICanMoveToPositionChecker
{
    bool CanGoTo(Vector2 vector);
}