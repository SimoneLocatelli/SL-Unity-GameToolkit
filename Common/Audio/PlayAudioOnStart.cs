using UnityEngine;

[RequireComponent(typeof(AudioManager))]
internal class PlayAudioOnStart : BaseBehaviour
{
    public string AudioName;

    private void Start()
    {
        if (string.IsNullOrWhiteSpace(AudioName)) return;

        AudioManager.Play(AudioName);
    }
}