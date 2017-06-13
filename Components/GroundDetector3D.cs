
using System;
using UnityEngine;
using Labo;


// 現状だとかなりキツイ斜面でも着地できてしまうが
// メッシュの角度などを使うことで制限したい。


namespace Fiziks3D
{
    public interface IGroundDetector3D
    {
        GroundingInformation3D Info { get;  }
        SlopeInformation3D SlopeInfo { get; }
    }


    public class GroundDetector3D : MonoBehaviour, IGroundDetector3D
    {
        public GroundingInformation3D Info { get; private set; }
        private SphereCastSetting3D setting;
        public SlopeInformation3D SlopeInfo { get; private set; }


        private void Start()
        {
            Info = new GroundingInformation3D();
            int[] thisLayer = { this.gameObject.layer };
            int mask = LayerMaskGenerator.Generate(LayerMaskGenerator.Ignore.SpecifiedLayer, thisLayer);
            setting = new SphereCastSetting3D(0.5f, 0.8f, Vector3.zero, mask);
            SlopeInfo = new SlopeInformation3D();
        }


        private void Update()
        {
            RaycastHit hitInfo;
            Vector3 origin = this.gameObject.transform.position + setting.Offset;

            // SphereCast の注意点...キャストが開始された時点ですでに重なっているオブジェクトは検出されない。
            // out がまだ正直よくわかっていない。
            Physics.SphereCast(origin,  setting.Radius,  Vector3.down,  out hitInfo,  setting.RayLength,  setting.Mask,  QueryTriggerInteraction.Ignore );

            // GroundingInformation クラスは RaycastHit をもとに情報を整理する。
            Info.Update(hitInfo);

            // 勾配についての情報を更新する。新しく追加。
            SlopeInfo.Update(hitInfo);

            // Debug.Log(Info.IsGrounding);
        }
    }
}