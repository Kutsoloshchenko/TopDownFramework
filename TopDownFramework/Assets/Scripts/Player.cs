using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownFramework 
{
    public class Player : Character
    {
        [HideInInspector]
        public CircleCollider2D col;
        [HideInInspector]
        public Rigidbody2D rb;
        [HideInInspector]
        public Transform tr;
        [HideInInspector]
        public Camera camera;


        protected Movement movement;



        // Start is called before the first frame update
        void Start()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            col = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            tr = GetComponent<Transform>();
            camera = Camera.main;
            movement = GetComponent<Movement>();
        }
    }

}

