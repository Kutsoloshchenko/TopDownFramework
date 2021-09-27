using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownFramework.Interface;
using System;
using TopDownFramework.Weapons;
using CubeWar.Enums;

namespace TopDownFramework
{
    public class Attack : Ability
    {
        [SerializeField]
        protected GameObject currentWeapon;

        // Start is called before the first frame update
        protected override void Initialization()
        {
            base.Initialization();
        }

        // Update is called once per frame
        void Update()
        {
            if (ShootingPressed())
            {
                currentWeapon.GetComponent<IWeapon>().Attack(tr.position, tr.rotation);
            }
        }

        protected void FixedUpdate()
        {
            
        }

        private bool ShootingPressed()
        {
            var attackMode = currentWeapon.GetComponent<IWeapon>().attackMode;

            switch (attackMode)
            {
                case AttackMode.Single:
                    return Input.GetMouseButtonDown(0);

                case AttackMode.Automatic:
                default:
                    return Input.GetMouseButton(0);
            }

        }
    }
}


