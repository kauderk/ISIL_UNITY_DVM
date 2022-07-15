using UnityEngine;
using System;
public enum SoundClipPlayOrder
{
    in_order,
    reverse,
    random
}
public enum AudioMood
{
    Background,
    Pickups,
    Fails,
    EpicFails,
    Wins
}

[System.Serializable]
public class InputOutputData
{
    [SerializeField]
    public Tuple<string, int> Track = new Tuple<string, int>("", 0);
    public AudioClip Clip;
    [HideInInspector]
    public AudioSource Source;
    [HideInInspector]
    public GameObject GameObj;
    public Other Other = new Other();
}
[System.Serializable]
public class TrackInstances
{
    public AudioSource Source;
    public GameObject GameObj;
}
[System.Serializable]
public class Other // Srializables....
{
    public bool Persist = false;
    public float Timestamp = 0.0f;
    public bool Loop_ = false;
    public int PreviousMax = 0;
    public int PreviousIndex = 0;
    public bool UseSemitones;
    public Vector2Int Semitones = new Vector2Int(0, 0);
    public Vector2 Pitch = new Vector2(1, 1);
    public SoundClipPlayOrder PlayOrder;
    public int PlayIndex = 0;
    public Vector2 Volume = new Vector2(0.5f, 0.5f);
}
