#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using System;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using TrackDict = RotaryHeart.Lib.SerializableDictionary.UDictionary<float, SO_AudioTrack>;

public static class TrackUtils
{
    public static int Get(this UDictionary<AudioMood, int> previousMoodIndex, AudioMood mood, UDictionary<AudioMood, TrackDict> lookupDict)
    {
        int previous = previousMoodIndex.TryGetValue(mood, out previous) ? previous : 0;
        previous = (previous + 1) % lookupDict[mood].Count;
        return previousMoodIndex[mood] = previous;
    }
    public static SO_AudioTrack TryGetGreaterTrack(this UDictionary<AudioMood, TrackDict> audioMoods, AudioMood mood, int score)
    {
        // get the value of the mood form the Udictionary
        var list = audioMoods.ToList(mood);
        if (list.Count == 0)
            return null;

        // find the first one greater than the score
        var track = list.Aggregate(list[0], (acc, crr) => crr.Threshold >= score ? crr : acc);
        if (track == null)
        {
            Debug.Log("No Audio found for score " + score);
            return null;
        }

        return track;
    }
    private const float SEMITONES_TO_PITCH_CONVERSION_UNIT = 1.05946f;
    public static void Config(this SO_AudioTrack IO_Audio, AudioSource source)
    {
        var vol = IO_Audio.Other.Volume;
        source.clip = IO_Audio.Clip; // GetNextClip TODO:
        source.time = IO_Audio.Other.Persist ? IO_Audio.Other.Timestamp : 0;
        source.volume = Random.Range(vol.x, vol.y);
        source.pitch = IO_Audio.Other.UseSemitones
            ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, Random.Range(IO_Audio.Other.Semitones.x, IO_Audio.Other.Semitones.y))
            : Random.Range(IO_Audio.Other.Pitch.x, IO_Audio.Other.Pitch.y);
        source.loop = IO_Audio.Other.Loop_;
    }
    public static AudioSource TryGetSource(this SO_AudioTrack IO_Audio)
    {
        if (!IO_Audio.Clip)
        {
            Debug.Log($"Audio Clip is null {IO_Audio.name}");
            return null;
        }
        if (IO_Audio.Clip.length == 0)
        {
            Debug.Log($"Missing sound clips for {IO_Audio.name}");
            return null;
        }

        var source = IO_Audio.instances.Source;
        if (source == null)
        {
            var _obj = new GameObject($"AudioSource {IO_Audio.name}", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        return source;
    }
    public static void SourceCleanup(SO_AudioTrack track)
    {
        void TryToDestroy(UnityEngine.Object obj)
        {
            if (!obj)
            {
                // Debug.Log($"trying to destroy a null instance at {track.name}");
                return;
            }
#if UNITY_EDITOR
            UnityEngine.Object.DestroyImmediate(obj);
#else
            UnityEngine.Object.Destroy(obj);
#endif
        }

        TryToDestroy(track.instances.Source);
        TryToDestroy(track.instances.Wrapper);
    }
    public static SO_AudioTrack GetTrack(this UDictionary<AudioMood, TrackDict> audioMoods, AudioMood mood, int index)
    {
        var list = audioMoods.ToList(mood);
        if (list.Count == 0)
            return null;
        return list[index];//.Find(IO => IO.Id == id);
    }
    public static List<SO_AudioTrack> ToList(this UDictionary<AudioMood, TrackDict> audioMoods, AudioMood mood)
    {
        var dict = audioMoods.TryGetValue(mood, out TrackDict value) ? value : null;
        if (dict == null)
        {
            Debug.Log("No Audio list found for mood " + mood);
            return null;
        }
        return dict.Values.ToList();
    }
    public static AudioClip GetNextClip(this SO_AudioTrack IO_Audio, List<SO_AudioTrack> list)
    {
        var playIndex = IO_Audio.Other.PlayIndex;
        var length = list.Count;

        // find next clip
        switch (IO_Audio.Other.PlayOrder)
        {
            case SoundClipPlayOrder.in_order:
                playIndex = (playIndex + 1) % length;
                break;
            case SoundClipPlayOrder.random:
                playIndex = Random.Range(0, length);
                break;
            case SoundClipPlayOrder.reverse:
                playIndex = (playIndex + length - 1) % length;
                break;
        }
        IO_Audio.Other.PlayIndex = playIndex;
        var track = list[playIndex >= length ? 0 : playIndex];

        return track.Clip;
    }
    public static void AssignSceneComponents(this UDictionary<AudioMood, TrackDict> audioMoods, AudioMixer MainAudioMixer)
    {
        ForEachEnum<AudioMood>((key, name) =>
        {
            (GameObject go, AudioSource src) = GetNonSerializableObjects(name); // those under Master

            src.outputAudioMixerGroup = MainAudioMixer.FindMatchingGroups(name)[0];

            audioMoods[key]
                .Values
                .ToList()
                .ForEach(track =>
                {
                    track.instances.Source = src;
                    track.instances.Wrapper = go;
                });
        });
    }
    public static void TryToDestroyTrackInstances(this UDictionary<AudioMood, TrackDict> audioMoods)
    {
        ForEachEnum<AudioMood>((key, name) => audioMoods[key]
            .Values
            .ToList()
            .ForEach(track => SourceCleanup(track)));
    }

    public static void ForEachEnum<TE>(Action<TE, string> action) where TE : Enum
    {
        string[] array = Enum.GetNames(typeof(TE));
        for (int i = 0; i < array.Length; i++)
        {
            var name = array[i];
            var enumKey = (TE)Enum.Parse(typeof(TE), name);
            action(enumKey, name);
        }
    }

    static (GameObject, AudioSource) GetNonSerializableObjects(string _name)
    {
        GameObject go; AudioSource sourcePreviewer;

#if UNITY_EDITOR
        go = EditorUtility
            .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                typeof(AudioSource));
        sourcePreviewer = go.GetComponent<AudioSource>();
#else
        // get the audio mixer group that matches the name
        go = new GameObject($"AudioPreview_{_name}");
        sourcePreviewer = go.AddComponent<AudioSource>();
#endif
        return (go, sourcePreviewer); // How do you return an object?
    }
}