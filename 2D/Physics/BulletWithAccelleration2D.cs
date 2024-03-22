using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWithAccelleration2D : Bullet2D
{
    #region Properties

    public float DisappearAfter = 1;
    public float InitialSpeed = 0;
    public float MaxSpeedSustain = 0.2f;
    private TimerCooldown _bulletTimer;

    private bool _isDecellerating;

    public float TimeToReachMaxSpeed = 1;

    #endregion Properties

    #region LifeCycle

    protected override void Update()
    {
        if (!Initialised || !CanMove || !enabled)
            return;

        if (_isDecellerating)
        {
            if (_bulletTimer.IsReady)
            {
                DebugLog("Stopped, waiting to disappear");

                DisableCollider();
                StartCoroutine(DisappearAfterWait());
                enabled = false;
                return;
            }
            _bulletTimer.Update();
        }
        else
        {
            if (_bulletTimer == null)
                _bulletTimer = new TimerCooldown(TimeToReachMaxSpeed + MaxSpeedSustain);
            else
            {
                _bulletTimer.Update();
                if (_bulletTimer.IsReady)
                {
                    DebugLog("Starting decellaration");
                    _bulletTimer.Reset();
                    _isDecellerating = true;
                }
            }
        }

        var timePercentage = _isDecellerating ? _bulletTimer.InversePercentage : _bulletTimer.Percentage;
        var lerpedSpeed = Mathf.Lerp(InitialSpeed, Speed, timePercentage);
        var transf = transform;
        var transfPos = transform.position;
        var targetPosition = transfPos + Direction;// * lerpedSpeed;
        transf.position = transfPos.MoveTowards(targetPosition, lerpedSpeed);
    }

    private IEnumerator DisappearAfterWait()
    {
        DebugLog("Will wait " + DisappearAfter + " seconds");
        yield return new WaitForSeconds(DisappearAfter);
        DebugLog("Destroying");
        Destroy();
        yield return null;
    }

    #endregion LifeCycle
}