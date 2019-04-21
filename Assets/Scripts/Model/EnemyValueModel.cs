using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    
    [Serializable]
    public class EnemyValueModel
    {
        public List<EnemyData> EnemyList;
    }

    [Serializable]
    public class EnemyData
    {
        public string PrefabName;
        public int Life;
        public int Attack;

        public void Copy(EnemyData data)
        {
            PrefabName = data.PrefabName;
            Life = data.Life;
            Attack = data.Attack;
        }
    }
    /// <summary>
    /// 怪物基础数值数据类
    /// </summary>
    public class EnemyDataModel
    {
        public Dictionary<EnemyId, EnemyData> DataDic;
    }
}
