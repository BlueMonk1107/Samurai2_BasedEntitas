using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Manager;
using Model;
using UIFrame;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// 数据模型管理类
    /// </summary>
    public class ModelManager : SingletonBase<ModelManager>
    {
        /// <summary>
        /// 玩家数据配置类
        /// </summary>
        public PlayerDataModel PlayerData { get; private set; }

        public HumanSkillModel HumanSkillModel { get; private set; }

        public EnemyModel EnemyModel { get; private set; }

        public SpawnEnemyModel SpawnEnemyModel { get; private set; }

        public EnemyDataModel EnemyDataModel { get; private set; }


        public void Init()
        {
            PlayerData = ConfigManager.Single.LoadJson<PlayerDataModel>(ConfigPath.PLAYER_CONFIG);
            HumanSkillModel = ConfigManager.Single.LoadJson<HumanSkillModel>(ConfigPath.HUMAN_SKILL_CONFIG);
            EnemyModel = ConfigManager.Single.LoadJson<EnemyModel>(ConfigPath.ENEMY_CONFIG);
            SpawnEnemyModel = ConfigManager.Single.LoadJson<SpawnEnemyModel>(ConfigPath.SPAWN_ENEMY_CONFIG);
            InitEnemyDataModel();
        }

        private void InitEnemyDataModel()
        {
            EnemyDataModel = new EnemyDataModel();
            EnemyDataModel.DataDic = new Dictionary<EnemyId, EnemyData>();

            EnemyValueModel model = ConfigManager.Single.LoadJson<EnemyValueModel>(ConfigPath.ENEMY_VALUE_CONFIG);
            EnemyData data = null;

            foreach (EnemyId id in Enum.GetValues(typeof(EnemyId)))
            {
                data = model.EnemyList.FirstOrDefault(u => u.PrefabName == id.ToString());
                if (data == null)
                {
                    Debug.Log("无法找到匹配项，名称为"+id);
                }
                else
                {
                    EnemyDataModel.DataDic.Add(id, data);
                }
            }
        }
    }
}
