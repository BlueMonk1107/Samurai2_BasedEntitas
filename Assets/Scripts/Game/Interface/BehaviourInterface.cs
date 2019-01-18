

namespace Game
{
    /// <summary>
    /// 基础行为接口
    /// </summary>
    public interface IBehaviour
    {
        void Forward();
        void Back();
        void Left();
        void Right();
    }

    /// <summary>
    /// 玩家行为接口
    /// </summary>
    public interface IPlayerBehaviour : IBehaviour
    {
        /// <summary>
        /// 攻击键（按下K）
        /// </summary>
        void AttackO();
        /// <summary>
        /// 攻击键 （按下L）
        /// </summary>
        void AttackX();
    }
}
