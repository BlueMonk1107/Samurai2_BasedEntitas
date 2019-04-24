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
        /// <summary>
        /// 生命值
        /// </summary>
        public int Life;
        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack;
        /// <summary>
        /// 攻击范围
        /// </summary>
        public float AttackRange;
        /// <summary>
        /// 安全距离
        /// </summary>
        public float SafeDistance;
        /// <summary>
        /// 发现目标的侦测范围
        /// </summary>
        public float FindDistance;
        /// <summary>
        /// 移动速度
        /// </summary>
        public float MoveSpeed;

        public void Copy(EnemyData data)
        {
            PrefabName = data.PrefabName;
            Life = data.Life;
            Attack = data.Attack;
            AttackRange = data.AttackRange;
            SafeDistance = data.SafeDistance;
            FindDistance = data.FindDistance;
            MoveSpeed = data.MoveSpeed;
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
