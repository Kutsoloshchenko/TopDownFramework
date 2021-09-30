using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownFramework.Interface;
using TopDownFramework.Enums;
using UnityEngine;

namespace TopDownFramework.Weapons
{
    public class Handgun : MonoBehaviour, IWeapon
    {

        [SerializeField]
        private GameObject projectile;

        [SerializeField]
        private float projectileSpeed;

        [SerializeField]
        private float attackInterval;

        [SerializeField]
        private float projectileLifeSpan;

        [SerializeField]
        private float OffsetValue;

        private float timeSinceLastShot = 0;

        public AttackMode attackMode { get; private set; } = AttackMode.Automatic;

        public void Attack(Vector3 attackerPossition, Quaternion attackerRotation, Vector3 targetPossition)
        {
            if (CanShoot())
            {
                StartCoroutine(Shoot(attackerPossition, attackerRotation, targetPossition));
            }
        }

        private bool CanShoot()
        {
            return (timeSinceLastShot <= 0);
        }

        private IEnumerator<object> Shoot(Vector3 attackerPossition, Quaternion attackerRotation, Vector3 targetPossition)
        {
            var x = targetPossition.x - attackerPossition.x;
            var y = targetPossition.y - attackerPossition.y;
            var xToYRealation = Mathf.Abs(x / y);

            var yVelocity = Mathf.Sqrt(Mathf.Pow(projectileSpeed, 2) / (Mathf.Pow(xToYRealation, 2) + 1));
            var offset = Mathf.Sqrt(Mathf.Pow(OffsetValue, 2) / (Mathf.Pow(xToYRealation, 2) + 1));
            var xVelocity = yVelocity * xToYRealation;

            var xLocalOffset = offset*xToYRealation;
            var yLocalOffset = offset;

            if (x < 0)
            {
                xVelocity *= -1;
                xLocalOffset *= -1;
                
            }

            if (y < 0)
            {
                yVelocity *= -1;
                yLocalOffset *= -1;
            }

            var projectileObject = Instantiate(projectile, new Vector3(attackerPossition.x + xLocalOffset, attackerPossition.y + yLocalOffset, attackerPossition.z), attackerRotation);

            projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);

            Destroy(projectileObject, projectileLifeSpan);

            timeSinceLastShot = attackInterval;
            yield return new WaitForSeconds(attackInterval);
            timeSinceLastShot = 0;
        }


    }
}
