//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{


    /// <summary>
    /// コンポーネントではない。視点変更の角度制限などをおこなう。
    /// </summary>
    public class LookAroundCalculator
    {

        private float upwardLimitEuler;
        private float downwardLimitEuler;


        public LookAroundCalculator(float upwardLimit = 70f, float downwardLimit = 70f)
        {
            #region eulerAngles の数値について
            //  x 軸の回転は transform.eulerAngles を見ると、
            // 正面をゼロとして、プラスが下方向となる。
            // 90が真下、180が真後ろを逆立ちして見ている状態、240が真上、360はゼロ...つまり正面。
            // なので少し上を向くと320などとなる。
            #endregion
            upwardLimitEuler = 360f - upwardLimit;
            downwardLimitEuler = downwardLimit;
        }


        public float LimittingVerticalRotation(GameObject rotatingObject, float rotation)
        {
            float currentAngle = rotatingObject.transform.eulerAngles.x;
            float filteredRotation =  rotation;

            if (currentAngle < upwardLimitEuler &&
              currentAngle > 180f &&
              rotation < 0)
            {
                filteredRotation = 0;
            }

            if (currentAngle > downwardLimitEuler &&
                currentAngle < 180f &&
                rotation > 0)
            {
                filteredRotation = 0;
            }

            return filteredRotation;
        }


        /// <summary>
        /// 上下左右の回転のみで、Z軸の(ドアノブをひねるような)回転はしないRotate。
        /// </summary>
        /// <param name="objectToRotate"></param>
        /// <param name="horizontalRotation"></param>
        /// <param name="verticalRotation"></param>
        public static void AntiTiltRotate(GameObject objectToRotate, float horizontalRotation, float verticalRotation)
        {
            // 回転がローカル軸に対してか、ワールド軸に対してか指定することができる。
            // Y軸方向の回転はワールド軸でないと、あちこち回転しているうちにオブジェクトが傾いてしまう。
            objectToRotate.transform.Rotate(0, horizontalRotation, 0, Space.World);
            objectToRotate.transform.Rotate(verticalRotation, 0, 0);
        }
    }


}