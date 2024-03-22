using UnityEngine;

public class Collision2DProxy : MonoBehaviour
{
    public delegate void OnCollisionEnter2DEvent(Collision2D collision);

    public delegate void OnTriggerEnter2DEvent(Collider2D collision);

    public event OnCollisionEnter2DEvent OnCollisionEnter;

    public event OnTriggerEnter2DEvent OnTriggerEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision);
    }
}