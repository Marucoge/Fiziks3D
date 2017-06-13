
using System;
using UnityEngine;


namespace Fiziks3D
{
    /// <summary>
    /// 足場となっているオブジェクトの勾配についての情報。勾配の角度や、現在の勾配に立っていられるか、など。
    /// 自作の他クラスと依存せず、独立している。
    /// </summary>
    public class SlopeInformation3D
    {
        public enum SlopeType { Standable, Unstandable, NotDetected}

        public float StandableLimit { get; private set; }
        public float SlopeDegree { get; private set; }
        public SlopeType Standable { get; private set; }

        

        public SlopeInformation3D()
        {
            StandableLimit = 45f;
        }


        public void Update(RaycastHit hitInfo)
        {
            Standable = IsStandableSlope(hitInfo);

            // デバッグ用
            Debug.Log(Standable);
        }


        // 勾配の角度を得るために、接地面の情報が必要。
        // よって、立てない角度の地面であっても一応接地はしている(IsGrounding == true)という
        // 扱いにする必要があると考えて、GroundDetector とは処理を切り離した。
        private SlopeType IsStandableSlope(RaycastHit _hitInfo)
        {
            // 接地自体をしていない場合。
            if (_hitInfo.collider == null) { return SlopeType.NotDetected; }

            // Vector3.Angle() はふたつのベクトル間の(鋭角の)角度を計算する。
            float slopeDegree = Vector3.Angle(Vector3.up, _hitInfo.normal);

            // デバッグ用
            //Debug.Log("SlopeDegree : " + slopeDegree);

            if (slopeDegree > StandableLimit) { return SlopeType.Unstandable; }
            return SlopeType.Standable;

        }
    }
}