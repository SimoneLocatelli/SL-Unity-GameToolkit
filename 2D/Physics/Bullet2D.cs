using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : BaseBehaviour2D
{
    #region Properties

    public float AngleOffset;
    public bool CanMove;
    public int Damage = 1;
    public float DestroyAfter = 3;
    public float ImpulseForce = 0;
    public float Speed = 10;

    [TagSelector]
    public List<string> Tags;

    protected float TimeSinceFired;
    protected Vector3 Direction { get; private set; }
    protected bool Initialised { get; private set; }

    #endregion Properties

    #region Methods

    public virtual void Setup(Vector2 spawnPosition, Vector2 targetPoint, List<string> targetedTags, int layerId)
    {
        transform.position = spawnPosition;
        Setup(targetPoint, targetedTags, layerId);
    }

    public virtual void Setup(Vector2 targetPoint, List<string> targetedTags, int layerId)
    {
        var position = transform.position;
        var direction = targetPoint;
        var angle = position.AngleBetweenPoints(direction);
        Direction = transform.position.GetDirection(targetPoint);
        transform.eulerAngles = new Vector3(0f, 0f, angle + AngleOffset);
        Tags = targetedTags;
        gameObject.layer = layerId;
        Initialised = true;
    }

    public virtual void Setup(Transform parent, Vector2 targetPoint, List<string> targetedTags, int layerId)
    {
        transform.parent = parent;
        Setup(targetPoint, targetedTags, layerId);
    }

    internal void Fire()
    {
        CanMove = true;
    }

    protected virtual void HandleCollision(GameObject obj)
    {
        if (!enabled) return;

        if (!obj.HasAnyTag(Tags))
            return;

        LifeSystemHandler.ApplyDamage(obj, Damage);

        if (ImpulseForce > 0)
            obj.GetComponent<Rigidbody2D>().AddForce(Direction * ImpulseForce, ForceMode2D.Force);

        Destroy();
    }

    #endregion Methods

    #region LifeCycle

    protected virtual void OnCollisionEnter2D(Collision2D collision)
        => HandleCollision(collision.gameObject);

    protected virtual void OnTriggerEnter2D(Collider2D collider)
        => HandleCollision(collider.gameObject);

    protected virtual void Update()
    {
        if (!Initialised || !CanMove || !enabled)
            return;

        if (TimeSinceFired >= DestroyAfter)
        {
            Destroy();
            return;
        }

        var transf = transform;
        var transfPos = transform.position;
        var targetPosition = transfPos + Direction;
        transf.position = transfPos.MoveTowards(targetPosition, Speed);
        TimeSinceFired += Time.deltaTime;
    }

    #endregion LifeCycle
}