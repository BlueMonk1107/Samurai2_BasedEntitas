using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 相机动画状态 根据此状态的改变播放相机动画
    /// </summary>
    [Game,Event(EventTarget.Self),Unique]
    public class CameraState : IComponent
    {
        public CameraAniName State;
    }

 
}
