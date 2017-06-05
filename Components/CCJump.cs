//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using System;
using UnityEngine;

namespace Fizix
{

    /// <summary>
    /// コンポーネント。接地判定のインターフェイスに依存している。
    /// </summary>
    public class CCJump : MonoBehaviour, IMovementCalculator
    {

        public Vector3 FixedMovement { get; set; }
        public Vector3 MovementPerFrame { get; set; }
        public float UpwardPower { get; set; }
        public bool Jumping { get; set; }
        private IGroundDetector detector;


        private void Start()
        {
            detector = GetComponent<IGroundDetector>();
            // テスト用
            UpwardPower = 15f;
        }


        private void Update()
        {

            // ジャンプ中に接地したらジャンプ終了。
            if (Jumping && detector.IsGrounding)
            {
                Jumping = false;
                MovementPerFrame = Vector3.zero;
            }

            // 接地中にジャンプボタンが押されたらジャンプ中ということに。
            if (detector.IsGrounding && Input.GetButton("Jump"))
            {
                Jumping = true;
            }

            // ジャンプ中なら上方向への移動量を設定。
            // 移動自体はそれを担当するクラスがフレーム内での移動量を合計していっぺんに行う。
            // 接地していない間は加速度的に重力が働くので放物線を描いて落ちる。
            if (Jumping)
            {
                MovementPerFrame = UpwardPower * Vector3.up;
            }

        }


    }
}