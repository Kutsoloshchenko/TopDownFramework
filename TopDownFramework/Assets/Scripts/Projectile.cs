using TopDownFramework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownFramework
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField]
        private float dmgValue;

        private void OnTriggerEnter2D(Collider2D other)
        {

            var otherIDamagable = other.GetComponent<IDamagable>();

            if (otherIDamagable != null)
            {
                otherIDamagable.ApplyDamage(dmgValue);
            }

            Destroy(gameObject);
        }


    }
}
