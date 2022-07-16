using System;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSFX", menuName = "Game Data/WeaponSFX")]
    public class SO_WeaponSFX : ScriptableObject
    {
        public AudioClip Fire;
        public AudioClip Release;
        public AudioClip Reload;
        public AudioClip Empty;
        public AudioClip Burst;
        public AudioClip Exhaust;
        public AudioClip PickUp;
        public AudioClip Drop;
        public AudioClip Equip;
        public AudioClip Unequip;

        public void Play(AudioClip clip)
        {
            if (clip == null)
                return;
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }
        public void Play(Func<SO_WeaponSFX, AudioClip> cb) => this.Play(cb?.Invoke(this));
    }
}
