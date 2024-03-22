using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static AttributeExtensions;

[System.Serializable]
public class Sound
{
    #region Settings Fields

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Sounds with the same name will be randomly selected to add variance.")]
    private string name;

    [SerializeField]
    private AudioClip clip;

    [ReadOnlyProperty]
    public AudioSource source;

    [SerializeField]
    private AudioMixerGroup mixerGroup;

    [Header("Playback Settings")]
    [SerializeField]
    private bool enabled = true;

    [SerializeField]
    public bool playOnceInEditMode = false;

    [SerializeField]
    private bool loop = false;

    [MinMaxRange(0.1f, 1f)]
    [SerializeField]
    private Vector2 volume = new Vector2(0.1f, 1f);

    [MinMaxRange(0.1f, 3f)]
    [SerializeField]
    private Vector2 pitch = new Vector2(0.1f, 3f);

    #endregion Settings Fields

    #region Props

    public string Name => name;

    public bool Enabled => enabled;

    public AudioClip Clip => clip;

    public bool Loop => loop;

    public float MinPitch => pitch.x;

    public float MaxPitch => pitch.y;

    public float MinVolume => volume.x;

    public float MaxVolume => volume.y;

    #endregion Props

    public Sound()
    {
        volume = GetRangeVector(nameof(volume));
        pitch = GetRangeVector(nameof(pitch));
    }

    private Vector2 GetRangeVector(string fieldName)
        => this.GetAttribute<MinMaxRangeAttribute>(fieldName, MemberTypes.Field).ToVector2();

    internal Sound Clone()
    {
        return new Sound
        {
            name = this.name,
            clip = this.clip,
            pitch = this.pitch,
            volume = this.volume,
        };
    }

    public void Update()
    {
        source.clip = Clip;
        source.loop = Loop;
        source.playOnAwake = false;
        source.pitch = Random.Range(MinPitch, MaxPitch);
        source.volume = Random.Range(MinVolume, MaxVolume);
    }
}