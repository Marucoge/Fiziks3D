//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{
    /// <summary>
    /// 重力による移動量の計算を行うクラスのための interface 。
    /// </summary>
    public interface IGravityCalculator
    {
        float GravityAccel { get; set; }
        float CalcMovement(float floatingTime);
    }
}