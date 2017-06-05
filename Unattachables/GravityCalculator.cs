using System;
using UnityEngine;

namespace Fizix
{


    /// <summary>
    /// コンポーネントではない。
    /// </summary>
    public class GravityCalculator : IGravityCalculator
    {

        public float GravityAccel { get; set; }


        public GravityCalculator()
        {
            GravityAccel = 30f;
        }


        public float CalcMovement(float floatingTime)
        {
            return floatingTime * GravityAccel;
        }

    }


}