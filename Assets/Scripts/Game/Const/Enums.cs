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
        START_GAME_ANI
    }

    /// <summary>
    /// 相机父物体
    /// </summary>
    public enum CameraParent
    {
        START,
        IN_GAME
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
    /// 关卡部分ID
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
    /// 动画参数对应枚举
    /// </summary>
    public enum PlayerAniIndex
    {
        IDLE,
        RUN,
        WALK
    }

    /// <summary>
    /// 计时器标识
    /// </summary>
    public enum TimerId
    {
        MOVE_TIMER
    }


}
