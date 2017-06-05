
using UnityEngine;

namespace Fizix
{


    /// <summary>
    /// コンポーネントではない。
    /// </summary>
    public interface IWalk
    {
        float ForwardWalkSpeed { get; set; }
        float BackwardWalkSpeed { get; set; }
        float SideWalkSpeed { get; set; }
    }


}