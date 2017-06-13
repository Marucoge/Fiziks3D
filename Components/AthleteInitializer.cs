
using UnityEngine;
using Labo;

namespace Fiziks3D
{
    public class AthleteInitializer : MonoBehaviour
    {
        private void Awake()
        {
            // エディタ上でやると忘れるので。
            this.gameObject.layer = LayerMask.NameToLayer("Athlete");
            this.tag = TagManager.AthleteTag;
        }
    }
}