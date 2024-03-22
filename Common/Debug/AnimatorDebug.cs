using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class AnimatorDebug : MonoBehaviour
{
    [Header("Animator")]
    public Animator Animator;

    [ReadOnlyProperty]
    [SerializeField] private bool isInitialized;

    [ReadOnlyProperty]
    [SerializeField] private AnimatorControllerParameter[] parameters;

    [ReadOnlyProperty]
    [SerializeField] private bool fireEvents;

    [ReadOnlyProperty]
    [SerializeField] private bool isInTransition;

    [Header("Layer")]
    [Range(0, 20)]
    [SerializeField] private int layerIndex = 0;

    [ReadOnlyProperty]
    [SerializeField] private string layerName;

    [Header("Current Animator State Info")]
    [ReadOnlyProperty]
    [SerializeField] private AnimatorStateInfo currentAnimatorStateInfo;

    [ReadOnlyProperty]
    [SerializeField] private float currentNormalizedTime;

    [ReadOnlyProperty]
    [SerializeField] private float currentNormalizedTimePercentage;

    [Header("Current Clip Info")]
    [ReadOnlyProperty]
    [SerializeField] private AnimatorClipInfo currentClipInfo;

    [Header("Current Clip")]
    [ReadOnlyProperty]
    [SerializeField] private float currentClipLength;

    [ReadOnlyProperty]
    [SerializeField] private string currentClipName;

    [ReadOnlyProperty]
    [SerializeField] private AnimationEvent[] currentClipEvents;

    private void Update()
    {
        Animator = Animator != null ? Animator : GetComponent<Animator>();
        Assert.IsNotNull(Animator);

        layerIndex = Mathf.Min(layerIndex, Animator.layerCount);
        layerName = Animator.GetLayerName(layerIndex);

        isInitialized = Animator.isInitialized;
        fireEvents = Animator.fireEvents;
        parameters = Animator.parameters;
        isInTransition = Animator.IsInTransition(layerIndex);

        currentAnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(layerIndex);
        currentNormalizedTime = currentAnimatorStateInfo.normalizedTime;
        currentNormalizedTimePercentage = currentAnimatorStateInfo.normalizedTime * 100;
        currentClipInfo = Animator.GetCurrentAnimatorClipInfo(layerIndex).FirstOrDefault();

        currentClipLength = currentClipInfo.clip.length;
        currentClipName = currentClipInfo.clip.name;
        currentClipEvents = currentClipInfo.clip.events;
    }
}