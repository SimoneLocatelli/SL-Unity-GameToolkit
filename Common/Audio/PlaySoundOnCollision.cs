using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip AudioClip;

    [TagSelector]
    public List<string> TargetedTags;

    [Range(0, 1)]
    public float Volume = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayAudio(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayAudio(collision.gameObject);
    }

    private void PlayAudio(GameObject gameObject)
    {
        if (AudioClip == null)
            return;

        if (!TargetedTags.Any() || gameObject.HasAnyTag(TargetedTags))
            AudioManager.PlayClipAtCameraPoint(AudioClip, Volume);
    }
}