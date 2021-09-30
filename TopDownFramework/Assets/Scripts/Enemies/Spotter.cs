using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownFramework.Interface;
using UnityEngine;

namespace TopDownFramework
{
    public class Spotter : MonoBehaviour, IEnemyAi
    {
        [SerializeField]
        private float rotationSpeedInAngelsPerSeccond;
        [SerializeField]
        private float surveyAngle;
        [SerializeField]
        private float delayBetweenTurns;

        private FieldOfView fov;
        private Transform tr;
        private Rigidbody2D rd;
        private IWeapon weapon;

        private float originalPossition;
        private float currentTurn;
        private int direction = 1;
        private float currentDelay = 0;


        void Start()
        {
            fov = GetComponent<FieldOfView>();
            tr = GetComponent<Transform>();
            weapon = GetComponent<IWeapon>();

            originalPossition = tr.eulerAngles.z;
        }

        public void ApplyEnemyBehaviour()
        {
            if (fov.canSeeTarget)
            {
                currentDelay = 0;
                RotateToTarget(fov.target.transform.position);
                weapon.Attack(tr.position, tr.rotation, fov.target.transform.position);
            }
            else
            {
                SurveyArea();
            }
        }

        private void SurveyArea()
        {
            if (currentDelay <= 0)
            {
                if (currentTurn >= surveyAngle)
                {
                    direction *= -1;
                    currentDelay = delayBetweenTurns;
                    currentTurn = 0;
                }
                else
                {
                    var turn = Time.deltaTime * rotationSpeedInAngelsPerSeccond;
                    currentTurn += turn;
                    tr.Rotate(Vector3.forward, turn * direction);
                }
            }
            else
            {
                currentDelay -= Time.deltaTime;
            }               
        }

        private void RotateToTarget(Vector3 possitionToLookAt)
        {
            var vectorToLookAt = possitionToLookAt - tr.position;


            vectorToLookAt = vectorToLookAt.normalized;
            var angle = (Mathf.Atan2(vectorToLookAt.x, vectorToLookAt.y) * Mathf.Rad2Deg) * (-1);
            tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return;

        }
    }
}
