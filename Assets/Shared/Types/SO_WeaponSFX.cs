using System;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSFX", menuName = "Game Data/WeaponSFX")]
    public class SO_WeaponSFX : ScriptableObject
    {
        [Header("Shoot")]
        public AudioClip Fire;
        public AudioClip Release;
        public AudioClip Empty;
        public AudioClip Burst;
        public AudioClip Dry;

        [Header("Reload")]
        public AudioClip SingleReload;
        public AudioClip FullyReloaded;
        public AudioClip BusyReloading;

        [Header("Reaction")]
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
        // Shooter and Reloader have same function, how you abstract that?
        public void Play(Func<SO_WeaponSFX, AudioClip> cb) => this.Play(cb?.Invoke(this));
    }
}
