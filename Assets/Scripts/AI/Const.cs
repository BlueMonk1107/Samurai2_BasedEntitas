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
        /// 自身视线范围
        /// </summary>
        public const float SIGHT_LINE_RANGE = 60;
        /// <summary>
        /// 一击必杀伤害数值
        /// </summary>
        public const int INTANT_KILL_VALUE = 1000;
        /// <summary>
        /// 身体上下左右四个部分的角度范围
        /// </summary>
        public const int BODY_PART_RANGE = 30;
        /// <summary>
        /// 头的比例
        /// </summary>
        public const int HEAD_SCALE = 1;
        /// <summary>
        /// 身体的比例
        /// </summary>
        public const int BODY_SCALE = 2;
        /// <summary>
        /// 腿的比例
        /// </summary>
        public const int LEG_SCALE = 2;
        /// <summary>
        /// 整个身体的总比例数
        /// </summary>
        public const int ALL_BODY_SCALE = HEAD_SCALE + BODY_SCALE + LEG_SCALE;
    }
}
