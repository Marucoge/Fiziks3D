//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{

    public interface IMovementCalculator
    {
        Vector3 MovementPerFrame { get; set; }
        Vector3 FixedMovement { get; set; }
    }

}