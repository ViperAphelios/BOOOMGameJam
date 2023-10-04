using System;
using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.WSA;

namespace ZFramework.TestExample.Scripts
{
    public class CircleController : MonoBehaviour
    {
        public bool isOnGround;
        public bool inAir;
        public bool isFirstJump;
        public bool isSecondJump;
        public float firstJumpForce;
        public float secondJumpForce;
        private Rigidbody2D mRigidbody2D;
        public RaySensor2D raySensor;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }


        // Update is called once per frame
        private void Update()
        {
            if (isFirstJump && inAir && Input.GetButtonDown("Jump"))
            {
                isFirstJump = false;
                isSecondJump = true;
                mRigidbody2D.AddForce(Vector2.up * secondJumpForce, ForceMode2D.Impulse);
            }

            if (!inAir)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    mRigidbody2D.AddForce(new Vector2(0, 1) * (firstJumpForce * Time.deltaTime), ForceMode2D.Force);
                    inAir = true;
                    isFirstJump = true;
                }
            }

            if (inAir && isFirstJump && !isSecondJump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    mRigidbody2D.AddForce(new Vector2(0, 1) * (firstJumpForce * Time.deltaTime), ForceMode2D.Force);
                }
            }


            raySensor.Pulse();
            isOnGround = raySensor.GetNearestDetection();

            if (isOnGround)
            {
                ResetCircle();
            }
        }

        private void ResetCircle()
        {
            isFirstJump = false;
            isSecondJump = false;
            inAir = false;
        }
    }
}