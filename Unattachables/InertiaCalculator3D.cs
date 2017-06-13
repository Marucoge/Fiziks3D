
using System;
using UnityEngine;


namespace Fiziks3D
{
    // ローカル座標で移動が実行されてしまい困ったが、
    // MovementHandler の方で Translate(vector, Space.World) としたことで解決。Translate はデフォルトだとローカル座標で移動を行うらしい。
    // 最終的には、CharacterController の衝突判定が Translate だと正しく行われないため、characterController.Move(vector) を使った。

   // 慣性による移動量の限界値を設定していないため、高速で動く足場などからジャンプするととんでもない移動量になることがある。
   // のちのち設定する必要がある。MovementHandler 自体にも必要だろう。

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

            if (detector.Info.IsGrounding == false)
            {
                // 接地していない場合、前フレームでの足場情報を消してreturn。(同じ足場に着地した時、足場の速度を誤算しないよう)
                // 現在の慣性移動量で設置するまで移動し続ける。(現在空気抵抗なし)
                inertiaInfo.ClearLastInfo();
                return;
            } else {
                // 接地していたら、慣性による移動量をリセット。
                MovementPerFrame = Vector3.zero;
            }


            // 現フレームでの足場情報を得る。
            GameObject currentGround = detector.Info.LastDetectedGround;
            inertiaInfo.UpdateCurrentInfo(currentGround);

            // 現フレームでの足場と、前フレームでの足場が同じ場合、その足場の速度(フレームごとの移動量)をもとめる。
            if (inertiaInfo.CurrentGround == inertiaInfo.LastGround)
            {
                MovementPerFrame = inertiaInfo.CalcGroundVelocity();
                //MovementPerFrame = new Vector3(MovementPerFrame.x, 0.00f, MovementPerFrame.z);
            }

            // 現フレームでの足場を、前フレームでの足場とする。
            inertiaInfo.UpdateLastInfo(currentGround);
       }

    }
}