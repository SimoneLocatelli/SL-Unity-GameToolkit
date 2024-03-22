using UnityEngine;

public class BaseUnitBehaviour : BaseBehaviour
{
    private Collider2D _collider2D;

    [SerializeField]
    private float speed = 5;

    public Collider2D Collider
    {
        get
        {
            if (_collider2D == null)
                _collider2D = GetComponent<Collider2D>();
            return _collider2D;
        }
    }

    public float Speed { get => speed; protected set => speed = value; }
}
