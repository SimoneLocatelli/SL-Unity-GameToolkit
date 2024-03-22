using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

public class BlinkWhileInvulnerable : BaseBehaviour
{
    BlinkSprite BlinkSprite;
    LifeSystem lifeSystem;

    private void Start()
    {
        lifeSystem = TopParent.GetComponent<LifeSystem>();
        Assert.IsNotNull(lifeSystem);


        BlinkSprite = gameObject.AddComponentIfNotPresent<BlinkSprite>();
        lifeSystem.OnDamageReceived += OnDamageReceived;
    }

    private void OnDamageReceived(LifeSystem obj)
        => BlinkSprite.Blink(blinkWhileTrue: () => lifeSystem.IsInvulnerable);
}
