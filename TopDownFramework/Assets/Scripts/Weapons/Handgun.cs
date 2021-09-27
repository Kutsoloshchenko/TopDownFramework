using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownFramework.Interface;
using CubeWar.Enums;
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

        public void Attack(Vector3 possition, Quaternion angle)
        {
            if (CanShoot())
            {
                StartCoroutine(Shoot(possition, angle));
            }
        }

        private bool CanShoot()
        {
            return (timeSinceLastShot <= 0);
        }

        private IEnumerator<object> Shoot(Vector3 possition, Quaternion angle)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var x = mousePos.x - possition.x;
            var y = mousePos.y - possition.y;

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

            var projectileObject = Instantiate(projectile, new Vector3(possition.x + xLocalOffset, possition.y + yLocalOffset, possition.z), angle);

            projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);

            Destroy(projectileObject, projectileLifeSpan);

            timeSinceLastShot = attackInterval;
            yield return new WaitForSeconds(attackInterval);
            timeSinceLastShot = 0;
        }


    }
}
