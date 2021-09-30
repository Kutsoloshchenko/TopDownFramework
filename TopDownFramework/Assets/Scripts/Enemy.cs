using TopDownFramework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownFramework.Interface;

namespace TopDownFramework
{
    public class Enemy : Character, IDamagable
    {

        [SerializeField]
        protected float HP;

        protected FieldOfView fov;
        protected IEnemyAi Ai;

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
            Ai = GetComponent<IEnemyAi>();
        }

        void FixedUpdate()
        {
            Ai.ApplyEnemyBehaviour();
        }
    }
}
