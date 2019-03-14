using UnityEngine;

namespace Game
{
    /// <summary>
    /// 相机动画状态名称
    /// </summary>
    public enum CameraAniName
    {
        NULL,
        /// <summary>
        /// 开场相机动画
        /// </summary>
        START_GAME_ANI,
        SHAKE_ANI,
        /// <summary>
        /// 相机跟随动画
        /// </summary>
        FOLLOW_PLAYER
    }

    /// <summary>
    /// 相机父物体
    /// </summary>
    public enum CameraParent
    {
        START,
        IN_GAME,
        /// <summary>
        /// 相机跟随动画
        /// </summary>
        FOLLOW_PLAYER
    }

    /// <summary>
    /// 关卡ID
    /// </summary>
    public enum LevelID
    {
        ONE =1,
        TWO
    }

    /// <summary>
    /// 关卡游戏部分大区域ID
    /// </summary>
    public enum LevelGamePartID
    {
        ONE = 1,
        TWO,
        THREE,
        FOUR,
        FIVE
    }

    /// <summary>
    /// 关卡游戏部分小区域ID
    /// </summary>
    public enum LevelPartID
    {
        ONE = 1,
        TWO
    }

    /// <summary>
    /// 输入按钮
    /// </summary>
    public enum InputButton
    {
        NULL,
        FORWARD,
        BACK,
        LEFT,
        RIGHT,
        ATTACK_O,
        ATTACK_X
    }

    /// <summary>
    /// 输入按键的按下状态
    /// </summary>
    public enum InputState
    {
        NULL,
        DOWN,
        PREE,
        UP  
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameState
    {
        START,
        PAUSE,
        END
    }

    /// <summary>
    /// 动画参数AniIndex对应枚举
    /// </summary>
    public enum PlayerAniIndex
    {
        IDLE,
        RUN,
        WALK
    }

    /// <summary>
    /// 人物行为
    /// </summary>
    public enum PlayerBehaviourIndex
    {
        IDLE,
        RUN,
        WALK,
        ATTACK
    }

    /// <summary>
    /// 状态机的行为状态
    /// </summary>
    public enum BehaviorState
    {
        ENTER,
        UPDATE,
        EXIT
    }

    /// <summary>
    /// 计时器标识
    /// </summary>
    public enum TimerId
    {
        MOVE_TIMER,
        /// <summary>
        /// 判断人物技能是否有效计时器
        /// </summary>
        JUDGE_SKILL_TIMER
    }

    /// <summary>
    /// 音效部分统一名称
    /// </summary>
    public enum AudioName
    {
        attack,
        injory,
        kotoul,
        step
    }

    public enum EnemyId
    {
        /// <summary>
        /// 近战小怪
        /// </summary>
        EnemyPeasant,
        /// <summary>
        /// 弓箭手
        /// </summary>
        EnemyBowman,
        /// <summary>
        /// 近战精英怪
        /// </summary>
        EnemySwordsman,
        /// <summary>
        /// Boss
        /// </summary>
        EnemyMiniBoss
    }
}
