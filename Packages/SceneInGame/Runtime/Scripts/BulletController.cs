using System.Diagnostics;
using UnityEngine;
using Photon.Pun;

namespace Weapon
{
    public class BulletController : MonoBehaviourPunBase
    {
        /// <summary>
        /// If no one sets a value, "settings.damage" will be used.
        /// </summary>
        [HideInInspector] public float? thisDamage = null;
        private SO_AmmoSettings settings;

        protected override void MyUpdate() => Move();

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            UnityEngine.Debug.Break();
            object[] instantiationData = info.photonView.InstantiationData;
            instantiationData[0] = instantiationData[0] as SO_AmmoSettings;
            settings = instantiationData[0] as SO_AmmoSettings;
            Init(settings);
            // ...
        }

        public void Init(SO_AmmoSettings settings) // Awake()
        {
            transform.position = settings.Instance.Origin.transform.position;
            thisDamage ??= settings.Damage;
            this.settings = settings;
        }

        float RandomJiggle() => Random.Range(settings.Jigle.x, settings.Jigle.y);
        private void Move()
        {
            var direccion = settings.OriginForward + new Vector3(RandomJiggle(), 0, RandomJiggle());
            var distance = Vector3.Distance(settings.Instance.Caster.transform.position, transform.position);
            transform.position += settings.Speed * Time.deltaTime * direccion;

            if (distance > settings.DestroyAtDistance)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamage>(out var damage))
                damage.TakeDamage(thisDamage ?? settings.Damage);

            settings.TriggerTags.ForEach(tag =>
            {
                if (other.gameObject.CompareTag(tag))
                    Destroy(this.gameObject);
            });
        }
    }
}
