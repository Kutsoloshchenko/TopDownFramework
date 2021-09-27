using CubeWar.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownFramework
{
    public class BreackableWall : MonoBehaviour, IDamagable
    {
        [SerializeField]
        public float HP { get; private set; }


        public void ApplyDamage(float dmgValue)
        {
            Destroy(gameObject);
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
