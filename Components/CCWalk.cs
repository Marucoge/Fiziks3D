//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using System;
using UnityEngine;

namespace Fizix
{


    public class CCWalk : MonoBehaviour, IWalk, IMovementCalculator
    {

        public float ForwardWalkSpeed { get; set; }
        public float BackwardWalkSpeed { get; set; } // 未対応
        public float SideWalkSpeed { get; set; }

        public Vector3 MovementPerFrame { get; set; }
        public Vector3 FixedMovement { get; set; }


        private void Start()
        {
            // テスト用
            ForwardWalkSpeed = 10f;
            BackwardWalkSpeed = 10f;
            SideWalkSpeed = 10f;
        }


        private void Update()
        {

            // 前と後ろで速度を変えたいが、今のところは同じ。
            MovementPerFrame =
                transform.forward * Input.GetAxis("Vertical") * ForwardWalkSpeed +
                transform.right * Input.GetAxis("Horizontal") * SideWalkSpeed
                ;

            FixedMovement = MovementPerFrame * Time.deltaTime;

        }

    }


}