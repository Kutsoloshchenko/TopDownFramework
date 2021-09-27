using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownFramework
{
    public class Movement : Ability
    {
        [SerializeField]
        protected float timeTillFullSpeed;

        [SerializeField]
        protected float maxSpeed;

        [SerializeField]
        protected float sprintMultiplier;

        [SerializeField]
        protected float walkMultiplier;

        public double currentSpeed = 0;

        public float currentHorizontalSpeed;
        public float currentVerticalSpeed;
        protected float acceleration;
        protected float runTime;
        private float horizontalInput;
        private float verticalInput;

        private const string horizontal = "Horizontal";
        private const string vertical = "Vertical";

        protected override void Initialization()
        {
            base.Initialization();
        }

        // Update is called once per frame
        void Update()
        {
            MovementPressed();
            SprintHeld();
            WalkHeld();
        }

        protected virtual void FixedUpdate()
        {
            MoveCharacter();

            // Maybe we need a different script that controls camera movement
            camera.transform.position = new Vector3(tr.position.x, tr.position.y, camera.transform.position.z);
        }

        protected virtual void MoveCharacter()
        {
            if (MovementPressed())
            {
                acceleration = maxSpeed / timeTillFullSpeed;
                var currentAcceleration = acceleration;
                bool strafing = horizontalInput != 0 && verticalInput != 0;

                if (strafing)
                {
                    currentAcceleration = acceleration / 2;
                }

                runTime += Time.deltaTime;
                currentHorizontalSpeed = horizontalInput * timeTillFullSpeed * currentAcceleration * runTime;
                currentVerticalSpeed = verticalInput * timeTillFullSpeed * currentAcceleration * runTime;

                currentSpeed = Math.Sqrt(Math.Pow(currentHorizontalSpeed, 2) + Math.Pow(currentVerticalSpeed, 2));

                if (currentSpeed > maxSpeed)
                {
                    if (strafing)
                    {
                        currentSpeed = maxSpeed;
                        float linearSpeed = (float)Math.Sqrt(Math.Pow(currentSpeed, 2) / 2);
                        currentHorizontalSpeed = linearSpeed * horizontalInput;
                        currentVerticalSpeed = linearSpeed * verticalInput;
                    }
                    else
                    {
                        if (Math.Abs(currentHorizontalSpeed) > maxSpeed)
                        {
                            currentHorizontalSpeed = maxSpeed * horizontalInput;
                            currentSpeed = Math.Abs(currentHorizontalSpeed);
                        }
                        if (Math.Abs(currentVerticalSpeed) > maxSpeed)
                        {
                            currentVerticalSpeed = maxSpeed * verticalInput;
                            currentSpeed = Math.Abs(currentVerticalSpeed);
                        }
                    }

                }


            }
            else
            {
                acceleration = 0;
                runTime = 0;
                currentHorizontalSpeed = 0;
                currentVerticalSpeed = 0;
                currentSpeed = 0;
            }

            SpeedMultiplier();
            Rotate();
            rb.velocity = new Vector2(currentHorizontalSpeed, currentVerticalSpeed);
        }

        protected virtual void SpeedMultiplier()
        {
            if (SprintHeld())
            {
                var localMultiplier = 1.0f;
                if (horizontalInput != 0 && verticalInput != 0)
                {
                    localMultiplier = ((float)Math.Sqrt(2)) / 2;
                }

                currentHorizontalSpeed *= sprintMultiplier * localMultiplier;
                currentVerticalSpeed *= sprintMultiplier * localMultiplier;
            }

            else if (WalkHeld())
            {
                var localMultiplier = 1.0f;
                if (horizontalInput != 0 && verticalInput != 0)
                {
                    localMultiplier = ((float)Math.Sqrt(2)) / 2;
                }
                currentHorizontalSpeed *= walkMultiplier * localMultiplier;
                currentVerticalSpeed *= walkMultiplier * localMultiplier;
            }
        }

        protected virtual bool MovementPressed()
        {
            horizontalInput = Input.GetAxis(horizontal);
            verticalInput = Input.GetAxis(vertical);

            if (horizontalInput != 0 || verticalInput != 0)
            {
                return true;
            }

            return false;
        }

        private void Rotate()
        {
            var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

            var vector = mousePos - tr.position;
            vector.Normalize();

            var angle = (Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg)*(-1);
            tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return;

        }

        protected virtual bool WalkHeld()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                return true;
            }
            return false;
        }

        protected virtual bool SprintHeld()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return true;
            }
            return false;
        }
    }
}

