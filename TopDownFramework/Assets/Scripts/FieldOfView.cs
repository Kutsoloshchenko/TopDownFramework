using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownFramework
{
    public class FieldOfView : MonoBehaviour
    {

        [SerializeField]
        public float radiusOfView;
        [SerializeField]
        public float angleOfView;


        [SerializeField]
        public GameObject target;

        [SerializeField]
        public bool canSeeTarget = false;

        public LayerMask targetMask;
        public LayerMask obstructionMask;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FOVRoutine());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
           var rangeCheck = Physics2D.OverlapCircle(transform.position, radiusOfView, targetMask);

            if (rangeCheck != null)
            {
                var target = rangeCheck.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;


                var tan = Mathf.Tan(transform.eulerAngles.z * Mathf.Deg2Rad)*-1;

                var y = Mathf.Sqrt(1 / ((Mathf.Pow(tan, 2) + 1)));

                if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
                {
                    y *= -1;
                }

                var x = y * tan;


                if (Vector3.Angle(new Vector3(x, y, transform.position.z), directionToTarget) < (angleOfView / 2))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeeTarget = true;
                        return;
                    }
                }
            }

            canSeeTarget = false;
        }
    }

}

