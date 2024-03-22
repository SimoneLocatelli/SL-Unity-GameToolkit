using UnityEngine;

internal class PlayAudioBehaviour : MonoBehaviour
{
    public string AudioName;
    private AudioManager _audioManager;

    private void PlayAudio()
    {
        if (_audioManager == null)
            _audioManager = GetComponentInParent<AudioManager>();

        _audioManager.Play(AudioName);
    }
}