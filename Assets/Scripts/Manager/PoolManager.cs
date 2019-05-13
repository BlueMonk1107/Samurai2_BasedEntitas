using Game.AI;
using Manager;
using Module.ObjectPool;
using UnityEngine;

namespace Game
{
    public class PoolManager : MonoBehaviour     
    {
        public static PoolManager Single { get; private set; }
        public GameObjectPool EffectPool { get; private set; }

        public void Awake()
        {
            Single = this;
        }

        public void Start()
        {
            InitEffectPool();
        }

        private void InitEffectPool()
        {
            GameObject effectParnet = new GameObject("EffectPool");
            effectParnet.transform.SetParent(transform);
            EffectPool = new GameObjectPool(effectParnet);
            GameObject temp = null;
            ObjectPoolData data;
            for (EffectNameEnum name = 0; name < EffectNameEnum.COUNT; name++)
            {
                temp = LoadManager.Single.Load<GameObject>(Path.EFFECTS_PATH, name.ToString());
                data = new ObjectPoolData(temp, name.ToString());
                EffectPool.AddSubPool(data);
            }
        }
    }
}
