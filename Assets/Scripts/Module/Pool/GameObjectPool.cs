using System.Collections.Generic;
using UnityEngine;
using Module.ObjectPool;

namespace Game
{
    public class GameObjectPool : ObjectPoolBase<PoolItem>
    {
        private GameObject _poolRoot;
        private Dictionary<string, GameObject> _parentDic; 

        public GameObjectPool(GameObject poolRoot)
        {
            _poolRoot = poolRoot;
            _parentDic = new Dictionary<string, GameObject>();
        }

        public override bool AddSubPool(ObjectPoolData data)
        {
            GameObject subPool = new GameObject(data.Name + "Pool");
            subPool.transform.SetParent(_poolRoot.transform);
            _parentDic[data.Name] = subPool;
            
            return base.AddSubPool(data);
        }

        protected override SubObjectPoolBase<PoolItem> GetNewSubPool(ObjectPoolData data)
        {
            return new SubPoolItemPool(data, _parentDic[data.Name]);
        }
    }

    public class SubPoolItemPool : SubObjectPoolBase<PoolItem>
    {
        private GameObject _parent;
        public SubPoolItemPool(ObjectPoolData data, GameObject parent) : base(data)
        {
            _parent = parent;
            PrespawnItem();
        }

        private void PrespawnItem()
        {
            for (int i = 0; i < _data.ActiveLimitNum; i++)
            {
                AddInactiveItem(SpawnNew());
            }
        }

        protected override void AddInactiveItem(PoolItem item)
        {
            base.AddInactiveItem(item);
            item.Hide();
        }

        protected override void AddActiveItem(PoolItem item)
        {
            base.AddActiveItem(item);
            item.Show();
        }

        protected override void DestroyItem(PoolItem obj)
        {
            obj.DestroyItem();
        }

        protected override PoolItem SpawnNew()
        {
            GameObject prefab = _data.Obj as GameObject;
            if (prefab != null)
            {
                GameObject obj = Object.Instantiate(prefab);
                obj.transform.SetParent(_parent.transform);
                PoolItem item = obj.AddComponent<PoolItem>();
                item.Name = _data.Name;
                return item;
            }
            
            return null;
        }
    }
}
