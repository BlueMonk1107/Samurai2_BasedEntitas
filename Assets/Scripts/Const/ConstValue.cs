using UnityEngine;

namespace Const
{
    public class ConstValue
    {
        public const string UI_NAMESPACE_NAME = "UIFrame";
        public const string UI_SCRIPT_POSTFIX = "View";
        public const string BUTTON_PARENT_NAME = "Buttons";

        public const string DIFFICULT_LEVEL = "DifficultLevel";
        public const string LEVEL_INDEX = "LevelIndex";
        public const string LEVEL_GAME_PART_INDEX = "LevelGamePartIndex";
        public const string LEVEL_PART_INDEX = "LevelPartIndex";

        public const string MAIN_SCENE = "Main";
        public const string COMICS_SCENE = "Comics";
        public const string LEVEL_SCENE = "Level";

        public const string GAME_PART_WALL = "Wall";
        public const string GAME_PART_SPWAN_POINT = "SpawnPoint";

        //ani para name
        /// <summary>
        /// 玩家动画参数名称
        /// </summary>
        public const string PLAYER_PARA_NAME = "AniIndex";
        /// <summary>
        /// 玩家技能动画参数名称
        /// </summary>
        public const string SKILL_PARA_NAME = "Skill";
        /// <summary>
        /// 是否是在攻击状态的Idle标志
        /// </summary>
        public const string IDLE_SWORD_PARA_NAME = "IsIdleSword";
        /// <summary>
        /// 技能名称前缀
        /// </summary>
        public const string SKILL_ANI_PREFIX = "attack";
        /// <summary>
        /// 技能起手时间
        /// </summary>
        public const float SKILL_START_TIME = 0.25f;
        /// <summary>
        /// 走一步的帧数
        /// </summary>
        public const int WALK_STEP_TIME = 20;
        /// <summary>
        /// 跑一步的帧数
        /// </summary>
        public const int RUN_STEP_TIME = 12;

        //声音音量
        /// <summary>
        /// 跑一步的帧数
        /// </summary>
        public const float MOVE_STEP_VOLUME = 0.4f;
    }
}
