

//using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace Fiziks3D
{
    public class CameraAngleHandler3D : MonoBehaviour
    {
        [SerializeField] private GameObject face;
        private CameraAngleCalculator3D calculator;
        public Vector2 Sensitivity { get; private set; }
        public Vector2 TotalRotation { get; private set; }
        private CameraAttacher3D attacher;


        private void Start()
        {
            calculator = new CameraAngleCalculator3D();
            Sensitivity = new Vector2(2.00f, 1.00f);
            TotalRotation = Vector2.zero;
            attacher = new CameraAttacher3D(face);
            attacher.AdjustCameraPosition(false);
        }

        private void Update()
        {
            TotalRotation = Vector2.zero;

            // CrossPlatformInputManager を利用する場合、エディタ上部に MobileInput という項目が現れ(?)、そこで
            // スマホ入力をテストするかの On/Off ができる。ビルド時は環境によって適切なものを選んでくれるっぽい。
            TotalRotation =
                Vector2.right * CrossPlatformInputManager.GetAxis("Mouse X") * Sensitivity.x +
                Vector2.up * CrossPlatformInputManager.GetAxis("Mouse Y") * Sensitivity.y * -1;

            // カメラの上下回転を制限する。
            float controlledVerticalRotation = calculator.LimittingVerticalRotation(face, TotalRotation.y);
            TotalRotation = new Vector2(TotalRotation.x, controlledVerticalRotation);
        }

        private void FixedUpdate()
        {
            // 角度に Time.deltatime をかけてもイイのかもしれないが、フレーム内で固定回数呼ばれるこれを使えば同じこと。
            CameraAngleCalculator3D.AntiTiltRotate(this.gameObject, TotalRotation.x, 0.00f);
            CameraAngleCalculator3D.AntiTiltRotate(face.gameObject, 0.00f, TotalRotation.y);
        }
    }
}