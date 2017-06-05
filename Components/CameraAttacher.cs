
using UnityEngine;


namespace Fizix
{


    public class CameraAttacher : MonoBehaviour
    {

        // カメラを縦回転させるとき、カメラを持った子オブジェクト(face)のみが縦回転する。
        [SerializeField] private GameObject face;
        [SerializeField] private bool FirstPersonAngle = false;


        private void Start()
        {
            AdjustCameraPosition(FirstPersonAngle);
        }


        private void AdjustCameraPosition(bool firstPersonAngle)
        {
            Camera.main.transform.parent = face.transform;
            Camera.main.transform.localRotation = Quaternion.identity;

            if (firstPersonAngle)
            {
                Camera.main.transform.localPosition = new Vector3(0, 1.8f, 0.5f);
                return;
            }

            Camera.main.transform.localPosition = new Vector3(0, 2.4f, -4f);
            Camera.main.transform.Rotate(new Vector3(6f, 0, 0));
        }


    }
}