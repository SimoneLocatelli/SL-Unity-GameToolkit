using UnityEngine;
using UnityEngine.Assertions;

public static class AnimatorExtensions
{
    public static bool IsPlayingAnimation(this Animator animator, string animationName)
    {
        Assert.IsNotNull(animator, nameof(animator));
        CustomAssert.IsNotNullOrWhitespace(animationName, nameof(animationName));
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}