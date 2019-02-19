

namespace Game
{
    /// <summary>
    /// 基础行为接口
    /// </summary>
    public interface IBehaviour
    {
        void Idle();
        /// <summary>
        /// 转向前方
        /// </summary>
        void TurnForward();
        /// <summary>
        /// 转向后方
        /// </summary>
        void TurnBack();
        /// <summary>
        /// 转向左侧
        /// </summary>
        void TurnLeft();
        /// <summary>
        /// 转向右侧
        /// </summary>
        void TurnRight();
        /// <summary>
        /// 移动
        /// </summary>
        void Move();
    }

    /// <summary>
    /// 玩家行为接口
    /// </summary>
    public interface IPlayerBehaviour : IBehaviour
    {
        /// <summary>
        /// 当前是否在跑标志位
        /// </summary>
        bool IsRun { get; set; }

        bool IsAttack { get;}

        /// <summary>
        /// 攻击键
        /// </summary>
        void Attack(int skillCode);
    }
}
