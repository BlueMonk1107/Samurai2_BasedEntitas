using Const;
using Manager;
using UnityEngine;

namespace Game.GamePart
{
    public class Part : MonoBehaviour
    {
        public void Init(LevelGamePartID gamePartId, LevelPartID id)
        {
            SpawnEnemyManager spawnManager = AddScript<SpawnEnemyManager>(ConstValue.GAME_PART_SPWAN_POINT);
            spawnManager?.Init();

            AddScript<PartWall>(ConstValue.GAME_PART_WALL)?.Init(gamePartId, id,()=> spawnManager.Spawn());
        }

        private T AddScript<T>(string name) where T:MonoBehaviour
        {
            var obj = transform.Find(name);
            if (obj != null)
            {
                return obj.gameObject.AddComponent<T>();
            }
            else
            {
                Debug.LogError("未找到Part下的"+ name + "父物体");
                return null;
            }
        }
    }
}
