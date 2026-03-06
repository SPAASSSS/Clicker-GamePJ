using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    public static UISoundPlayer Instance { get; private set; }

    [Header("Clips")]
    public AudioClip hoverClip; // your "click" sound on hover
    public AudioClip clickClip; // your "pop" sound on click

    [Header("Volume")]
    [Range(0f, 1f)] public float hoverVolume = 0.6f;
    [Range(0f, 1f)] public float clickVolume = 0.8f;

    private AudioSource _audio;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _audio = GetComponent<AudioSource>();
        if (_audio == null) _audio = gameObject.AddComponent<AudioSource>();

        _audio.playOnAwake = false;
        _audio.spatialBlend = 0f; // 2D sound
    }

    public void PlayHover()
    {
        if (hoverClip != null)
            _audio.PlayOneShot(hoverClip, hoverVolume);
    }

    public void PlayClick()
    {
        if (clickClip != null)
            _audio.PlayOneShot(clickClip, clickVolume);
    }
}