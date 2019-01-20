using Entitas;
using Entitas.CodeGeneration.Attributes;

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

    /// <summary>
    /// 游戏状态
    /// </summary>
    [Game,Unique]
    public class GameStateComponent : IComponent
    {
        public GameState GameState;
    }

    /// <summary>
    /// 玩家
    /// </summary>
    [Game,Unique]
    public class PlayerComponent : IComponent
    {
        public IView Player;
        public IPlayerBehaviour Behaviour;
        public IPlayerAni Ani;
    }

    /// <summary>
    /// 玩家动画
    /// </summary>
    [Game]
    public class PlayerAniState : IComponent
    {
        public PlayerAniIndex AniIndex;
    }
 
}
