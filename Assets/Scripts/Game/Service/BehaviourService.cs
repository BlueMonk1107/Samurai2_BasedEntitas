using UnityEngine;

namespace Game
{
    /// <summary>
    /// 基础行为接口
    /// </summary>
    public interface IBehaviour
    {
        void Up();
        void Down();
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

    public class PlayerBehaviourService : IPlayerBehaviour
    {
        public void Up()
        {
            throw new System.NotImplementedException();
        }

        public void Down()
        {
            throw new System.NotImplementedException();
        }

        public void Left()
        {
            throw new System.NotImplementedException();
        }

        public void Right()
        {
            throw new System.NotImplementedException();
        }

        public void AttackO()
        {
            throw new System.NotImplementedException();
        }

        public void AttackX()
        {
            throw new System.NotImplementedException();
        }
    }
}
