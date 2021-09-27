using CubeWar.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownFramework
{
    public class Enemy : Character, IDamagable
    {

        [SerializeField]
        protected float HP;


        public void ApplyDamage(float dmgValue)
        {

            HP -= dmgValue;

            if (HP <= 0)
            {
                Destroy(gameObject);
            }
            
        }



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
