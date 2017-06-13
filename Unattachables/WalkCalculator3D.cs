
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace Fiziks3D
{
    public class WalkCalculator3D : IMovementCalculator
    {
        public Vector3 MovementPerFrame { get; private set; }
        public float WalkSpeed { get; private set; }    // 将来的には前後左右で速度を変えたいかも。
        private GameObject walker;


        public WalkCalculator3D(GameObject _gameObject)
        {
            WalkSpeed = 10.00f;
            walker = _gameObject;
        }


        public void ManualUpdate()
        {
            float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
            float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

            // MovementHandler での Translate が ワールド座標で実行するよう指定してあるため、
            // Vector3.forward などとするとうまくいかない。
            MovementPerFrame =
                walker.transform.right * horizontalInput * WalkSpeed +
                 walker.transform.forward * verticalInput * WalkSpeed;
        }
    }
}