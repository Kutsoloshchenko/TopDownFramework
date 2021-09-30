using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownFramework.Interface;
using System;
using TopDownFramework.Weapons;
using TopDownFramework.Enums;

namespace TopDownFramework
{
    public class Attack : Ability
    {
        protected IWeapon currentWeapon;

        // Start is called before the first frame update
        protected override void Initialization()
        {
            base.Initialization();
            currentWeapon = GetComponent<IWeapon>();
        }

        // Update is called once per frame
        void Update()
        {
            if (ShootingPressed())
            {
                currentWeapon.Attack(tr.position, tr.rotation, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        protected void FixedUpdate()
        {
            
        }

        private bool ShootingPressed()
        {
            var attackMode = currentWeapon.attackMode;

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


