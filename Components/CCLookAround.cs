//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using System;
using UnityEngine;

namespace Fizix
{


    public class CCLookAround : MonoBehaviour
    {
        [SerializeField] private GameObject face;
        private LookAroundCalculator calculator;
        public float HorizontalSensitivity { get; private set; }
        public float VerticalSensitivity { get; private set; }
        private float horizontalRotation;
        private float verticalRotation;


        private void Start()
        {
            calculator = new LookAroundCalculator();

            // テスト用
            HorizontalSensitivity = 5f;
            VerticalSensitivity = 5f;
        }


        private void Update()
        {
            horizontalRotation = Input.GetAxis("Mouse X") * HorizontalSensitivity;
            verticalRotation = Input.GetAxis("Mouse Y") * VerticalSensitivity * -1;

            verticalRotation = calculator.LimittingVerticalRotation(face, verticalRotation);
        }


        private void FixedUpdate()
        {
            LookAroundCalculator.AntiTiltRotate(this.gameObject, horizontalRotation, 0f);
            LookAroundCalculator.AntiTiltRotate(face.gameObject, 0, verticalRotation);
        }
    }


}