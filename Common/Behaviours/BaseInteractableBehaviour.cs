using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseInteractableBehaviour : BaseBehaviour
{
    #region Properties

    public const string ActivateIconName = "ActivateIcon";
    public bool CanInteract = true;

    public AudioClip InteractAudio;
    public Action OnInteraction;
    private SpriteRenderer _activateIconSpriteRenderer;

    protected SpriteRenderer ActivateIconSpriteRenderer
    {
        get
        {
            if (_activateIconSpriteRenderer == null)
            {
                var activateIcon = transform.Find(ActivateIconName);
                Assert.IsNotNull(activateIcon, "An interactable item must have a child named 'ActivateIcon'");

                _activateIconSpriteRenderer = activateIcon.GetComponent<SpriteRenderer>();
                Assert.IsNotNull(_activateIconSpriteRenderer, "An interactable item must have a child named 'ActivateIcon' with a SpriteRenderer");
            }

            return _activateIconSpriteRenderer;
        }
    }

    private bool HasActivateIcon
    {
        get
        {
            if (_activateIconSpriteRenderer != null)
                return true;

            return transform.Find(ActivateIconName) != null;
        }
    }

    #endregion Properties

    #region Methods

    public void AddActivateIcon()
    {
        if (HasActivateIcon) return;

        var activateIconPrefab = GetActivateIconPrefabPath();
        var startPath = new Vector3(0, 1.5f);
        _activateIconSpriteRenderer = CustomResources.InstantiatePrefab<SpriteRenderer>(activateIconPrefab, startPath, parent: transform);
        _activateIconSpriteRenderer.gameObject.name = ActivateIconName;
        Assert.IsNotNull(_activateIconSpriteRenderer);
    }

    public abstract void Interact();

    public void SetFocus(bool enabled)
    {
        ActivateIconSpriteRenderer.enabled = enabled;
    }

    protected abstract string GetActivateIconPrefabPath();

    protected abstract AudioClip GetDefaultInteractAudio();

    protected void PlayAudio()
    {
        AudioManager.PlayClipAtCameraPoint(InteractAudio);
    }

    #endregion Methods

    #region LifeCycle

    private void Awake()
    {
        Assert.IsNotNull(GetComponent<Collider2D>(), "Every interactable must have a component inheriting from Collider2D.");

        SetFocus(false);
    }

    private void Reset()
    {
        InteractAudio = GetDefaultInteractAudio();
        AddActivateIcon();
    }

    #endregion LifeCycle
}