using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;
using Util;

namespace Game
{
    public class SpawnEnemyManager : MonoBehaviour
    {
        private HashSet<SpawnEnemy> activeEnemies;
        private HashSet<SpawnEnemy> inactiveEnemies;

        public void Init()
        {
            activeEnemies = new HashSet<SpawnEnemy>();
            inactiveEnemies = new HashSet<SpawnEnemy>();

            InitEnemy();
        }

        private void InitEnemy()
        {
            SpawnEnemy enemyTemp = null;
            int index = 1;
            foreach (Transform trams in transform)
            {
                enemyTemp = trams.GetOrAddComponent<SpawnEnemy>();
                enemyTemp.Init(index,RemoveEnemyCallBack);
                inactiveEnemies.Add(enemyTemp);
                index++;
            }
        }

        private void RemoveEnemyCallBack(SpawnEnemy enemy)
        {
            activeEnemies.Remove(enemy);
            Spawn();
        }

        public void Spawn()
        {
            int count = GetSpawnNum();
            SpawnEnemy enemyTemp = null;

            HashSet<SpawnEnemy>.Enumerator temp = inactiveEnemies.GetEnumerator();
            for (int i = 0; i < count; i++)
            {
                if (temp.MoveNext())
                {
                    enemyTemp = temp.Current;
                    activeEnemies.Add(enemyTemp);
                    enemyTemp.Spawn();
                }
            }

            foreach (SpawnEnemy enemy in activeEnemies)
            {
                inactiveEnemies.Remove(enemy);
            }
        }

        private int GetSpawnNum()
        {
            return ModelManager.Single.EnemyModel.SpawnLimitNum - activeEnemies.Count;
        }
    }
}
