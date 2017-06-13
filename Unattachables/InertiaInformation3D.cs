

//using System.Collections.Generic;
using UnityEngine;


namespace Fiziks3D
{
    public class InertiaInformation3D 
    {
        public GameObject LastGround { get; private set; }
        public Vector3 lastGroundPosition { get; private set; }

        public GameObject CurrentGround { get; private set; }
        public Vector3 currentGroundPosition { get; private set; }


        /// <summary>
        /// 現フレームでの足場情報を記録する。
        /// </summary>
        /// <param name="_currentGround"></param>
        public void UpdateCurrentInfo(GameObject _currentGround)
        {
            this.CurrentGround = _currentGround;
            this.currentGroundPosition = _currentGround.transform.position;
        }

        /// <summary>
        /// 前フレームでの足場情報を記録する。
        /// </summary>
        /// <param name="_lastGround"></param>
        public void UpdateLastInfo(GameObject _lastGround)
        {
            this.LastGround = _lastGround;
            this.lastGroundPosition = _lastGround.transform.position;
        }


        /// <summary>
        /// 前フレームでの足場情報を消す。
        /// </summary>
        public void ClearLastInfo()
        {
            this.LastGround = null;
            this.lastGroundPosition = Vector3.zero;    
        }

        /// <summary>
        ///  現フレームでの足場情報を消す。
        /// </summary>
        public void ClearCurrentInfo()
        {
            this.CurrentGround = null;
            this.currentGroundPosition = Vector3.zero;
        }

        /// <summary>
        /// 現フレーム、前フレーム両方の足場情報を null や ゼロにする。
        /// </summary>
        public void Clear()
        {
            ClearCurrentInfo();
            ClearLastInfo();
        }


        /// <summary>
        /// 足場となっているオブジェクトの移動速度(per frame)を求める。
        /// </summary>
        /// <returns></returns>
        public Vector3 CalcGroundVelocity()
        {
            Vector3 movementPerFrame = (currentGroundPosition - lastGroundPosition) / Time.deltaTime;
            return movementPerFrame;
        }
    }
}