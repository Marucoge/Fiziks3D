
//using System.Collections.Generic;
using System;
using UnityEngine;


namespace Fiziks3D
{
    public class GravityCalculator3D : IMovementCalculator
    {
        public Vector3 MovementPerFrame { get; private set; }
        private IGroundDetector3D detector;
        public float FloatingTime { get; private set; }
        public float GravityAccel { get; private set; }


        public GravityCalculator3D(GameObject _gameObject)
        {
            detector = _gameObject.GetComponent<IGroundDetector3D>();
            FloatingTime = 0.00f;
            GravityAccel = 8f;
        }


        public void ManualUpdate()
        {
            if (detector.Info == null) { return; }

            // 接地していたらフィールドをリセット。
            if (detector.Info.IsGrounding)
            {
                MovementPerFrame = Vector3.zero;
                FloatingTime = 0.00f;
                return;
            }

            // 接地していない場合、経過時間を計って、経過時間*加速度 ぶん下方向に移動させる。
            FloatingTime += Time.deltaTime;
            MovementPerFrame =  FloatingTime * GravityAccel * Vector3.down;
        }
    }
}