

//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Fiziks3D
{
    public class JumpCalculator3D : IMovementCalculator
    {
        public Vector3 MovementPerFrame { get; private set; }
        private IGroundDetector3D detector;
        public float UpwardPower { get; private set; }
        public bool Jumping { get; private set; }


        public JumpCalculator3D(GameObject _gameObject)
        {
            detector = _gameObject.GetComponent<IGroundDetector3D>();
            UpwardPower = 18.00f;
        }


        public void ManualUpdate()
        {
            // エラー処理。
            if(detector.Info == null) { return; }

            // 接地したらジャンプ終了、ジャンプ移動量をゼロに。
            if (detector.Info.IsGrounding)
            {
                Jumping = false;
                MovementPerFrame = Vector3.zero;
            }

            // 接地中にジャンプボタンが押されたらジャンプ中ということにする。
            if (detector.Info.IsGrounding && 
                CrossPlatformInputManager.GetButton("Jump"))
            {
                Jumping = true;
            }

            // ジャンプ中だったら、ジャンプ移動量を設定する。
            if (Jumping)
            {
                MovementPerFrame = UpwardPower * Vector3.up;
            }
        }
    }
}