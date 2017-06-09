
using UnityEngine;



namespace Fiziks3D
{
    public class CameraAttacher3D : MonoBehaviour
    {
        [SerializeField] private GameObject face;
        [SerializeField] private bool FirstPersonAngle;

        private void Start()
        {
            AdjustCameraPosition();
        }

        /// <summary>
        /// カメラを正しい位置に配置する。プレイ中に複数回、様々なタイミングで呼ばれる可能性がある。
        /// </summary>
        private void AdjustCameraPosition()
        {
            Camera.main.transform.parent = face.transform;
            Camera.main.transform.localRotation = Quaternion.identity;

            if (FirstPersonAngle)
            {
                Camera.main.transform.localPosition = new Vector3(0, 1.8f, 0.5f);
                return;
            }

            Camera.main.transform.localPosition = new Vector3(0, 2.4f, -4f);
            Camera.main.transform.Rotate(new Vector3(6f, 0, 0));
        }
    }
}