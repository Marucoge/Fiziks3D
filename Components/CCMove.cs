//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{

    /// <summary>
    /// 入力、重力、慣性などの移動を合計して、移動を実行するコンポーネント。
    /// </summary>
    public class CCMove : MonoBehaviour
    {

        private CharacterController character;
        private IMovementCalculator walk;
        private IMovementCalculator gravity;
        private IMovementCalculator jump;
        private IMovementCalculator inertia;

        public Vector3 TotalMovement { get; private set; }


        private void Start()
        {
            character = GetComponent<CharacterController>();
            
            // クラスを特定する必要は無いか。
            walk = GetComponent<CCWalk>();
            gravity = GetComponent<GravitySimulator>();
            jump = GetComponent<CCJump>();
            inertia = GetComponent<InertiaSimulator>();
        }


        // (仮) 書いてみているだけ
        private void FindCalculators()
        {
            IMovementCalculator[] calculators = GetComponents<IMovementCalculator>();
        }

        
        private void Update()
        {
            TotalMovement = 
                ( walk.MovementPerFrame  + 
                gravity.MovementPerFrame + 
                jump.MovementPerFrame +
               inertia.MovementPerFrame )  * Time.deltaTime;

            // はじめは複数のスクリプトのUpdate() 内でMove を呼んでいたが、
            // 1フレーム内で増減を繰り返すためか、よくかすかな振動が起きていた。
            // その対策としてMove を呼ぶのはフレームごとにここで一回だけとしてみる。
            character.Move(TotalMovement);
        }

    }


}