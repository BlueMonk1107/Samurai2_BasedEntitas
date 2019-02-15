using UnityEngine;

namespace Game
{
    /// <summary>
    /// 动画部分接口
    /// </summary>
    public interface IAni
    {
        void Play(int aniIndex);
    }

    public interface IPlayerAni : IAni,IPlayerBehaviour
    {

    }
}
