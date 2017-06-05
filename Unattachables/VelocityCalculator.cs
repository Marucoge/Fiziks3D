//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{

    /// <summary>
    /// コンポーネントではない。オブジェクトのフレームあたりの移動速度を得る。Rigidbody を持たないオブジェクト用。
    /// </summary>
    public class VelocityCalculator
    {

        public Vector3 LastPosition { get; private set; }
        public GameObject LastGround { get; private set; }

        public Vector3 CurrentPosition { get; private set; }
        public GameObject CurrentGround { get; private set; }


        public void UpdateCurrentInfo(GameObject currentGround)
        {
            this.CurrentGround = currentGround;
            this.CurrentPosition = currentGround.transform.position;
        }


        public void UpdateLastInfo(GameObject lastGround)
        {
            this.LastGround = lastGround;
            this.LastPosition = lastGround.transform.position;
        }


        public void ResetLastInfo()
        {
            LastGround = null;
            LastPosition = Vector3.zero;
        }

        
        public Vector3 CalcVelocity()
        {
            // Time.deltaTime で割る必要があったのか。
            return  (CurrentPosition - LastPosition) / Time.deltaTime;
        }


        public void DebugInfo()
        {
            Debug.Log(
            "CurrentGround : " + CurrentGround + " at " + CurrentPosition + "\n" +
            "LastGround : " + LastGround + " at " +LastPosition + "\n"
            );
        }
    }


}