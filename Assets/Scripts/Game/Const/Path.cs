using UnityEngine;

namespace Game
{
    public class Path      
    {
        /// <summary>
        /// resource下预制文件夹路径
        /// </summary>
        private const string PREFAB_PATH = "Prefab/";
        /// <summary>
        /// 玩家预制路径
        /// </summary>
        public const string PLAYER_PREFAB = PREFAB_PATH + "Player";
        /// <summary>
        /// 玩家预制路径
        /// </summary>
        public const string TRAILS_COMBO_PREFAB = PREFAB_PATH + "trails_combo01";
        /// <summary>
        /// 敌方预制路径
        /// </summary>
        public const string ENEMY_PATH =  "Enemies/";
        /// <summary>
        /// peasant切断头死亡动画
        /// </summary>
        public const string PEASANT_DEAD_HEAD_PATH = ENEMY_PATH + "DeadPeasantHHead";
        /// <summary>
        /// peasant切断身体死亡动画
        /// </summary>
        public const string PEASANT_DEAD_BODY_PATH = ENEMY_PATH + "DeadPeasantHBody";
        /// <summary>
        /// peasant切断腿死亡动画
        /// </summary>
        public const string PEASANT_DEAD_LEG_PATH = ENEMY_PATH + "DeadPeasantHLegs";
    }
}
