
using System;
using UnityEngine;


namespace Fiziks3D
{
    // ローカル座標で移動が実行されてしまい困ったが、
    // MovementHandler の方で Translate(vector, Space.World) としたことで解決。Translate はデフォルトだとローカル座標で移動を行うらしい。

    /// <summary>
    /// 慣性を簡易シミュレートする。接地判定のインターフェイスと、InertiaInformation3D クラスを使用している。
    /// </summary>
    public class InertiaCalculator3D : IMovementCalculator
    {
        public Vector3 MovementPerFrame { get; private set; }
        private IGroundDetector3D detector;
        private InertiaInformation3D inertiaInfo;
        

        public InertiaCalculator3D(GameObject _gameObject)
        {
            detector = _gameObject.GetComponent<IGroundDetector3D>();
            inertiaInfo = new InertiaInformation3D();
        }
        
            
        public void ManualUpdate()
        {
            // エラー処理
            if (detector.Info == null) { return; }

            // 接地していない場合、前フレームでの足場情報を消す。(同じ足場に着地した時、足場の速度を誤算しないよう)
            if (detector.Info.IsGrounding == false)
            {
                inertiaInfo.ClearLastInfo();
                return;
            }

            // 現フレームでの足場情報を得る。
            GameObject currentGround = detector.Info.LastDetectedGround;
            inertiaInfo.UpdateCurrentInfo(currentGround);

            // 現フレームでの足場と、前フレームでの足場が同じ場合、その足場の速度(フレームごとの移動量)をもとめる。
            if (inertiaInfo.CurrentGround == inertiaInfo.LastGround)
            {
                MovementPerFrame = inertiaInfo.CalcGroundVelocity();
                // MovementPerFrame = new Vector3(MovementPerFrame.x, 0.00f, MovementPerFrame.z);
            }

            // 現フレームでの足場を、前フレームでの足場とする。
            inertiaInfo.UpdateLastInfo(currentGround);
       }

    }
}