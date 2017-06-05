using System;
using UnityEngine;

namespace Fizix
{
    // このクラスは役割を持ちすぎているかも。接地判定スクリプトなどで状態遷移を管理させる?
    // あるいは状態遷移を判定、その情報を保持するクラスを作成する。
    // 接地中, 歩行中, ジャンプ中, 浮遊中, 着地, 離陸

    /// <summary>
    /// コンポーネント。VelocityCalculatorクラスと接地判定のインターフェイスに依存している。
    /// 慣性(?)を簡易シミュレートする。
    /// </summary>
    public class InertiaSimulator : MonoBehaviour, IMovementCalculator
    {

        public Vector3 FixedMovement { get; set; }
        public Vector3 MovementPerFrame { get; set; }

        private IGroundDetector detector;
        private VelocityCalculator calculator;


        private void Start()
        {
            detector = GetComponent<IGroundDetector>();
            calculator = new VelocityCalculator();
        }


        private void Update()
        {
            // 接地していない場合は何もしない(現在の移動量も変更しない)。
            // 同じ足場に着地した時に異常な移動量を得てしまわないよう、前フレームの足場情報を null にする。
            if (detector.IsGrounding == false)
            {
                calculator.ResetLastInfo();
                return;
            }

            // 浮遊後に接地した瞬間、慣性による移動量をリセットする。これで無限バウンドはもう起こらない。
            if (calculator.LastGround == null && detector.IsGrounding)
            {
                // Debug.Log("応急処置  " + MovementPerFrame);  // ゴムボール現象のとき、接地判定のたびにこのログが出力された。
                MovementPerFrame = Vector3.zero;
            }

            // このフレームで足場となっているオブジェクト。
            GameObject currentGround = detector.LastDetectedGround;

            // このフレームでの足場を "現在の足場" として記録する。
            calculator.UpdateCurrentInfo(currentGround);

            // 足場となっているオブジェクトが変わっていない場合 (現在の足場==前フレームでの足場)、
            // そのオブジェクトと同じ移動量を得る。
            if (calculator.CurrentGround == calculator.LastGround)
            {
                MovementPerFrame = calculator.CalcVelocity();
                // MovementPerFrame = new Vector3(MovementPerFrame.x, 0f, MovementPerFrame.z); // テスト中
            }

            //calculator.DebugInfo();

            // このフレームでの足場を、"前フレームでの足場" として記録する。
            calculator.UpdateLastInfo(currentGround);
        }
        

    }
}


#region 怪奇! ゴムボール現象 (解決済)
// ERROR !!!
// 高速で上下するエレベータ上でジャンプした際に、ぴたっと着地せず大きくバウンドすることがある。
// 少しで済むのならいいのだが、そのまま地上に降りてもバウンドし続ける現象が起こることがあり修正が必要。
// IMGUI を眺めると, InertiaMovementが接地しても空中にいてもずっと同じ数値のままだった。
// 再びエレベータに乗ったりしてみると治った。
// 原因は...
// 上方向の慣性による移動量がある状態で空中に放り出されたとしても、重力によって下方向へ徐々に加速していく。
// 接地していない間、今のところは "前フレームでの足場" の情報を消して(null を代入) return している。
// 落下していっても地面に触れたのが1フレームだけでそのまま(次フレームには上昇して)接地が終了したのなら、
// 接地した足場の移動量を計算しない(慣性による移動量が変化しない)ことになる。
// 対策は...
// とりあえず、 浮遊後に接地したときを "着地" として、その瞬間に慣性による影響をリセットしてみた。
// if ( 前フレームでの足場 == null  &&  接地した)
// 応急処置かテストのつもりだったが、これで全く問題なさそうに見える。
// すでに重力による落下量が 慣性による上方向の移動量を完全に上回った後でなぜ地面を離れるのかは分からないが...
// ちなみにゴムボール現象が起きている間は、接地するたびに上記の条件文がtrueとなった。
// うまくいってるけど...null かどうかチェックするって安全なんだろうか ?
// 浮遊後、というフラグをどこかで作っておいたほうがいいか。
#endregion
