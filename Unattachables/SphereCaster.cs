//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking; // (needs NetworkBehaviour)
//using UnityEngine.UI;
using UnityEngine;

namespace Fizix
{
    /// <summary>
    /// コンポーネントではない。接地判定で使うレイキャスト用のクラス。
    /// </summary>
    public class SphereCaster
    {

        public GameObject LastDetectedObject { get; private set; }
        // public GameObject ObjectBeingDetected { get; private set; }
        public Vector3 Offset { get; private set; }
        public float Radius { get; private set; }
        public float RayLength { get; private set; }

        private LayerMask mask;
        private RaycastHit hitInfo;


        public SphereCaster(int  layerMask = 1)
        {
            this.mask = layerMask;

            Offset = Vector3.zero;
            Radius = 0.5f;
            RayLength = 0.8f;
        }


        // やや応急的。
        public void IgnoreMyLayer(GameObject gObject)
        {
            // 1 << gameObject.layer で自分と同一のレイヤー以外を無視、
            // ~でビットをすべて反転(?) つまり自分と同一のレイヤーのみ無視
            this.mask = ~(1 << gObject.layer);
        }


        /// <summary>
        /// SphereCast はそのSphereの内側にあるCollider を検出しない。
        /// SphereCast のorigin地点からすでに何かと重なっている場合、そのCollider は無視されるので注意。
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        public bool Cast(Vector3 currentPosition)
        {
            Vector3 position = currentPosition + Offset;

            # region SphereCast についての注意点...
            // 内側にあるCollider を検出しない。厳密な意味で"接触"した時のみtrueを返す。
            // Notes: SphereCast will not detect colliders for which the sphere overlaps the collider.
            #endregion
            bool detected =
                Physics.SphereCast(position, Radius, Vector3.down, out hitInfo, RayLength, mask, QueryTriggerInteraction.Ignore);

            if (detected) { LastDetectedObject = hitInfo.collider.gameObject; }

            return detected;
        }
    }
}