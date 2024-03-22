using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Jukebox : BaseBehaviour
{
    #region Properties

    public bool fadeIn;
    public float fadeInLength = 5;

    [Range(0, 1)]
    public float maxVolume = 1;

    public bool SetRandomInitialPosition = true;
    public List<AudioClip> Songs;
    public float startDelay = 0;
    private AudioSource _audioSource;

    private float initialTime = 0;

    private float playTime = -1;

    [SerializeField, ReadOnlyProperty]
    private float startTime = -1;

    #endregion Properties

    #region Methods

    private AudioSource AudioSource
    {
        get => GetInitialisedComponent(ref _audioSource);
    }

    private AudioClip GetRandomSong()
    {
        var index = Random.Range(0, Songs.Count - 1);

        return Songs[index];
    }

    private void Start()
    {
        if (FindObjectsOfType<Jukebox>().Count() > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        if (fadeIn)
            AudioSource.volume = 0;
        else
            AudioSource.volume = maxVolume;

        if (startTime < 0)
            startTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (AudioSource.isPlaying)
        {
            if (fadeIn)
            {
                var elapsedTime = Time.timeSinceLevelLoad - playTime;
                var volume = elapsedTime / fadeInLength;
                AudioSource.volume = volume;

                if (volume >= maxVolume)
                    fadeIn = false;
            }
        }
        else
        {
            if (startDelay > 0 && (Time.timeSinceLevelLoad + initialTime - startTime) < startDelay)
                return;

            playTime = Time.timeSinceLevelLoad;
            AudioSource.clip = GetRandomSong();

            if (SetRandomInitialPosition)
                initialTime = Random.Range(0, AudioSource.clip.length);

            AudioSource.time = initialTime;
            AudioSource.Play();
        }
    }

    #endregion Methods
}