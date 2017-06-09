
using UnityEngine;

namespace Fiziks3D
{
    public class SphereCastSetting3D
    {
        public float Radius { get; private set; }
        public float RayLength { get; private set; }
        public Vector3 Offset { get; private set; }
        public int Mask { get; private set; }

        public SphereCastSetting3D(float radius, float rayLength, Vector3 offset, int mask = ~0)
        {
            this.Radius = radius;
            this.RayLength = rayLength;
            this.Offset = offset;
            this.Mask = mask;   // 初期値ではすべてのレイヤーと衝突(?)
        }
    }
}