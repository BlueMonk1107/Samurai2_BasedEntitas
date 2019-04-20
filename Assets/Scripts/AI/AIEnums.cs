using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 动作
    /// </summary>
    public enum ActionEnum
    {
        IDLE,
        IDLE_SWORD,
        ATTACK,
        DEAD,
        INJJURE,
        MOVE,
        MOVE_BACKWARD,
        ALERT
    }

    /// <summary>
    /// 目标
    /// </summary>
    public enum GoalEnum
    {
        IDLE_SWORD,
        ATTACK,
        DEAD,
        INJJURE
    }

    /// <summary>
    /// 状态键值
    /// </summary>
    public enum StateKeyEnum
    {
        ALERT,
        FIND_ENEMY,
        NEAR_ENEMY,
        ATTACK,
        MOVE,
        INJURE,
        DEAD,
        /// <summary>
        /// 后退
        /// </summary>
        STEP_BACK
    }

    /// <summary>
    /// 游戏数据键值
    /// </summary>
    public enum GameDataKeyEnum
    {
        /// <summary>
        /// 敌方单位对象
        /// </summary>
        ENEMY_TRANS,
        /// <summary>
        /// 自身对象
        /// </summary>
        SELF_TRANS,
        /// <summary>
        /// 受到的伤害值
        /// </summary>
        INJURE_VALUE,
        /// <summary>
        /// 生命值
        /// </summary>
        LIFE
    }
}
