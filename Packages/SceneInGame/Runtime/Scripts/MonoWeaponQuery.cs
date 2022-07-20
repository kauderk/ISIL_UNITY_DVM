using System;
using UnityEngine;

namespace Weapon
{
    public static class MonoWeaponQuery
    {
        public static (bool canShoot, Action TakeFromMagazine) CanShoot(this IWeaponShooter I, bool down, bool hold, SO_WeaponMagazine Magazine)
        {
            var automatic = hold && (I.Type == WeaponType.automatic);
            var semiAutoOrSingle = down && (I.Type != WeaponType.automatic); // so far there are only three
            var canShoot = automatic || semiAutoOrSingle;
            void TakeFromMagazine()
            {
                if (I.Type == WeaponType.singleShot)
                    Magazine.Clear(); // 0
                else
                    Magazine.Consume(); // 1
            }
            return (canShoot, TakeFromMagazine);
        }
        public static (bool down, bool up, bool hold, bool any) InputShoot(this KD_Shooter S)
        {
            var down = Input.GetButtonDown("Fire1");
            var up = Input.GetButtonUp("Fire1");
            var hold = Input.GetMouseButton(0);
            var any = down || hold;
            return (down, up, hold, any);
        }
        public static (bool fully, bool single, bool any) InputReload(this KD_Reloader R)
        {
            var fully = Input.GetKeyDown(KeyCode.R);
            var single = Input.GetKeyDown(KeyCode.T);
            var any = fully || single;
            return (fully, single, any);
        }
    }
}