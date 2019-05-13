using UnityEngine;

namespace Game.AI
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
        DEAD_HALF_BODY,
        DEAD_HALF_HEAD,
        DEAD_HALF_LEG,
        INJURE_UP,
        INJURE_DOWN,
        INJURE_LEFT,
        INJURE_RIGHT,
        MOVE,
        MOVE_BACKWARD,
        ALERT,
        ENTER_ALERT,
        EXIT_ALERT
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
        /// <summary>
        /// 是否发现目标
        /// </summary>
        FIND_ENEMY,
        /// <summary>
        /// 是否到达目标身边
        /// </summary>
        NEAR_ENEMY,
        /// <summary>
        /// 是否可以攻击
        /// </summary>
        CAN_ATTACK,
        /// <summary>
        /// 是否能够向前移动
        /// </summary>
        CAN_MOVE_FORWARD,
        /// <summary>
        /// 是否受伤
        /// </summary>
        IS_INJURE,
        /// <summary>
        /// 是否死亡
        /// </summary>
        IS_DEAD,
        /// <summary>
        /// 是否在安全距离
        /// </summary>
        IS_SAFE_DISTANCE,
        /// <summary>
        /// 是否处在警戒状态
        /// </summary>
        IS_ALERT,
        /// <summary>
        /// 结束行动
        /// </summary>
        IS_OVER
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
        /// 此单位的基础配置文件
        /// </summary>
        CONFIG,
        /// <summary>
        /// 声音源组件
        /// </summary>
        AUDIO_SOURCE,
        /// <summary>
        /// 动画组件
        /// </summary>
        ANIMATION,
        /// <summary>
        /// ai部分数据管理类
        /// </summary>
        AI_MODEL_MANAGER,
        /// <summary>
        /// unity触发器脚本
        /// </summary>
        UNITY_TRIGGER,
        /// <summary>
        /// 受伤碰撞判定数据（字典）
        /// </summary>
        INJURE_COLLODE_DATA
    }
    /// <summary>
    /// Peasant怪物动画名称
    /// </summary>
    public enum AIPeasantAniName
    {
        idle,
        idleSword,
        showSword,
        hideSword,
        runSwordBackward,
        runSword,
        attackPeasant,
        injuryFront01,
        injuryFront02,
        injuryFront03,
        injuryFront04,
        death01,
        death02,
        DeadPeasantHHead,
        DeadPeasantHBody,
        DeadPeasantHLegs
    }

    public enum EffectNameEnum
    {
        Spawn,
        BloodOnGround,
        Dead,
        InjureLeft,
        InjureRight,
        COUNT
    }

    public enum AudioNameEnum
    {
        attack,
        death,
        injory,
        spawn
    }
}
