using UnityEngine;
using UnityEngine.Audio;
using System;
using RotaryHeart.Lib.SerializableDictionary;

using TrackDict = RotaryHeart.Lib.SerializableDictionary.UDictionary<float, SO_AudioTrack>;

// https://forum.unity.com/threads/solved-but-unhappy-scriptableobject-awake-never-execute.488468/#post-3188178

[CreateAssetMenu(fileName = "AudioManager", menuName = "Managers/AudioManager")]
public class AudioManager : SingletonScriptableObject<AudioManager>
{
    public AudioMixer MainAudioMixer;
    public bool PlayOnLoad;
    [field: SerializeField]
    public UDictionary<AudioMood, int> PreviousMoodIndex { get; private set; }
    public UDictionary<AudioMood, TrackDict> audioMoods;
    public static Action<AudioMood, int> OnScoreChanged;
    public static Action<AudioMood, int> OnScoreChangedThreshold;

    void OnEnable() => MainManager.OnGameInitialized += OnLoad;

    private void OnLoad()
    {
        audioMoods.AssignSceneComponents(MainAudioMixer);

        if (PlayOnLoad)
            PlayNextTrack(AudioMood.Background);

        OnScoreChangedThreshold += TryToPlayNextTrack;
        OnScoreChanged += PlayNextIfScoreChanged;
    }

    void OnDisable()
    {
        OnScoreChangedThreshold -= TryToPlayNextTrack;
        OnScoreChanged -= PlayNextIfScoreChanged;
        MainManager.OnGameInitialized -= OnLoad;

        audioMoods.TryToDestroyTrackInstances();
    }

    private void TryToPlayNextTrack(AudioMood mood, int score)
    {
        var list = audioMoods.ToList(mood);
        for (int i = 0; i < list.Count; i++)
        {
            var next = list[(i + 1) % list.Count]; // get the next one by it's index wihout stack overflow
            if (score > next.Threshold)
            {
                next.instances.Source.Play();
                return;
            }
        }
    }

    void PlayNextIfScoreChanged(AudioMood mood, int score)
    {
        var track = audioMoods.TryGetGreaterTrack(mood, score);
        if (track)
            Play(track); // BE Carefull, give it a key, else it will spam the hell out of it
    }
    private void PlayNextTrack(AudioMood mood)
    {
        var track = audioMoods.GetTrack(mood, PreviousMoodIndex.Get(mood, audioMoods));
        Play(track);
    }

    SO_AudioTrack GetTrack0() => audioMoods.GetTrack(AudioMood.Background, 0);
    public void PlayPreview()
    {
        if (!GetTrack0().Clip)
            OnLoad(); // Debug.Log("source was null on play, it's better to create a new one");
        Play(GetTrack0());
    }
    public void StopPreview()
    {
        var track = GetTrack0();
        if (track.instances.Source)
            track.instances.Source.Stop();
    }

    public AudioSource Play(SO_AudioTrack track)
    {
        var source = track.TryGetSource();
        if (!source)
            return null;

        track.Config(source);
        source.Play();
        return track.instances.Source = source; // cache
    }
}
