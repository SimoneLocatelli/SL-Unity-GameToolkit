using UnityEngine;

public class SpriteShake : BaseBehaviour
{
    private Vector3? _originalPosition;
    private float shakingElapsedTime;

    public float duration = 0.5f;
    private bool _isShaking;
    public float magnitude = 0.1f;

    public bool ShakeOnDamage = true;

    public float cooldown = 0;

    private TimerCooldown shakeCooldownTimer;

    private void OnEnable()
    {
        shakeCooldownTimer = new TimerCooldown(0);

        if (ShakeOnDamage)
        {
            var lifeSystem = GetComponent<LifeSystem>();
            lifeSystem.OnDamageReceived += OnDamageReceived;
        }
    }

    private void OnDamageReceived(LifeSystem obj)
    {
        if (_isShaking) return;

        if (shakeCooldownTimer.IsReady)
            _isShaking = true;

        shakingElapsedTime = 0;
        _originalPosition = transform.position;
    }

    private void Update()
    {
        //if (shakeCooldownTimer.IsReady)
        //{
        //    if (_isShaking)
        //    {
        //        if (elapsedTime < duration)
        //        {
        //            var newPosition = CalculateNewPosition();
        //            transform.localPosition = newPosition;
        //            elapsedTime += Time.deltaTime;
        //        }
        //        else
        //        {
        //            Reset();
        //            shakeCooldownTimer.Reset(cooldown);
        //        }
        //    }
        //}
        //else
        //{
        //    shakeCooldownTimer.Update();
        //}

        if (_isShaking)
        {
            if (shakingElapsedTime < duration)
            {
                var newPosition = CalculateNewPosition();
                transform.localPosition = newPosition;
                shakingElapsedTime += Time.deltaTime;
            }
            else
            {
                Reset();
            }
        }
        else
        {
            shakeCooldownTimer.Update();
        }
    }

    private Vector3 CalculateNewPosition()
    {
        var localPosition = _originalPosition.Value;
        float xOffset = localPosition.x + Random.Range(-0.5f, 0.5f) * magnitude;
        float yOffset = localPosition.y + Random.Range(-0.5f, 0.5f) * magnitude;
        var v = localPosition.Update(x: xOffset, y: yOffset);
        return v;
    }

    private void Reset()
    {
        _originalPosition = null;
        _isShaking = false;
        shakingElapsedTime = 0f;
        shakeCooldownTimer.Reset(cooldown);
    }
}