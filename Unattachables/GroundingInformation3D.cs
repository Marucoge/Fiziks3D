

//using System.Collections.Generic;
using UnityEngine;

namespace Fiziks3D
{
    public class GroundingInformation3D 
    {
        public bool IsGrounding { get; private set; }
        public RaycastHit HitInfo { get; private set; }
        public GameObject LastDetectedGround { get; private set; }
        public GameObject GroundBeingDetected { get; private set; }


        /// <summary>
        /// RaycastHit の情報を整理してフィールドに反映する。
        /// </summary>
        /// <param name="hitInfo"></param>
        public void Update(RaycastHit hitInfo)
        {
            this.HitInfo = hitInfo;

            if (HitInfo.collider == null)
            {
                // 接地していない場合
                GroundBeingDetected = null;
                IsGrounding = false;
            } else {
                // 接地している場合
                GameObject detectedObject = HitInfo.collider.gameObject;
                GroundBeingDetected = detectedObject;
                LastDetectedGround = detectedObject;
                IsGrounding = true;
            }

        }
    }
}