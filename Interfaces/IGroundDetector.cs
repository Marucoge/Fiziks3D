//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{
    /// <summary>
    /// 接地判定の
    /// </summary>
    public interface IGroundDetector
    {
        bool IsGrounding { get; set; }
        GameObject LastDetectedGround { get; set; }
    }
}