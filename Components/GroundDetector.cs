
using UnityEngine;

namespace Fizix
{

    /// <summary>
    /// コンポーネント。接地判定を行う。接地判定のインターフェイスを実装している。SphereCasterクラスに依存している。
    /// </summary>
    public class GroundDetector : MonoBehaviour, IGroundDetector
    {

        public bool IsGrounding { get; set; }
        public GameObject LastDetectedGround { get; set; }
        private SphereCaster caster;
        

        private void Start()
        {
            caster = new SphereCaster();
            caster.IgnoreMyLayer(this.gameObject);
        }


        private void Update()
        {
            IsGrounding = caster.Cast(this.transform.position);
            LastDetectedGround = caster.LastDetectedObject;
        }

    }
}