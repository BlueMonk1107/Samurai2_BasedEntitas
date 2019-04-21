using UnityEngine;

namespace Game.AI
{
    public class Const      
    {
        /// <summary>
        /// 攻击待机状态的延时时间
        /// </summary>
        public const float IDLE_SWORD_DELAY_TIME = 2;
        /// <summary>
        /// 临近敌方的可攻击距离
        /// </summary>
        public const float NEAR_ENEMY_DISTANCE = 1.5f;
        /// <summary>
        /// 安全距离
        /// </summary>
        public const float SAFE_DISTANCE = 5;
        /// <summary>
        /// 自身实现范围
        /// </summary>
        public const float SIGHT_LINE_RANGE = 60;
        /// <summary>
        /// 发现目标的距离
        /// </summary>
        public const float FIND_DISTANCE = 20;
        /// <summary>
        /// 移动速度
        /// </summary>
        public const float MOVE_VELOCITY = 1.5f;
        /// <summary>
        /// 一击必杀伤害数值
        /// </summary>
        public const int INTANT_KILL_VALUE = 1000;
    }
}
