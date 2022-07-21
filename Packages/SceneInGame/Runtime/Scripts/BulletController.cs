using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

namespace Weapon
{
    public class BulletController : MonoBehaviourPunBase, IPunInstantiateMagicCallback
    {
        [TagSelector]
        public List<string> TriggerTags = new List<string>();
        Vector3 initPos = Vector3.zero;
        Vector3 forward = Vector3.zero;

        //protected override void MyUpdate() => Move();

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            object[] instantiationData = info.photonView.InstantiationData;
            initPos = transform.position;
            forward = (Vector3)instantiationData[0];
        }


        float RandomJiggle() => Random.Range(-.3f, .3f);
        private void Update()
        {
            var direccion = forward + new Vector3(RandomJiggle(), 0, RandomJiggle());
            var distance = Vector3.Distance(initPos, transform.position);
            transform.position += 10 * Time.deltaTime * direccion;

            if (distance > 30)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamage>(out var damage))
                damage.TakeDamage(10);


            TriggerTags.ForEach(tag =>
            {
                if (other.gameObject.CompareTag(tag))
                    Destroy(this.gameObject);
            });
        }
    }
}
