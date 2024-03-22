using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class BlinkSprite : BaseBehaviour2D
{
    private Action _onFinishedBlinking;
    private bool _useDefaultDuration;
    private Func<bool> _blinkWhileTrue;
    private SpriteRenderer _sr;

    public float DefaultDuration = 1f;
    public TimerCooldown BlinkTimer;

    private IEnumerator BlinkInternal()
    {
        _sr = ParentSpriteRenderer;

        Assert.IsNotNull(_sr);

        bool isOpaque = false;

        ; while (_blinkWhileTrue())
        {
            var a = isOpaque ? 1 : 0.1f;
            isOpaque = !isOpaque;
            UpdateSpriteTransparency(a);
            yield return new WaitForSeconds(0.1f);
        }

        UpdateSpriteTransparency(1);

        if (_onFinishedBlinking != null)
            _onFinishedBlinking();

        yield return null;
    }

    private void UpdateSpriteTransparency(float alpha)
    {
        _sr.color = _sr.color.Update(a: alpha);
    }

    public void Blink(Action onFinishedBlinking = null, Func<bool> blinkWhileTrue = null)
    {
        _onFinishedBlinking = onFinishedBlinking;
        _useDefaultDuration = blinkWhileTrue == null;
        BlinkTimer.Reset(DefaultDuration);
        _blinkWhileTrue = _useDefaultDuration ? DefaultBlinkWhileTrue : _blinkWhileTrue;
        StartCoroutine(nameof(BlinkInternal));
    }

    private void Update()
    {
        BlinkTimer.Update();
    }

    private bool DefaultBlinkWhileTrue()
    {
        return !BlinkTimer.IsReady;
    }
}