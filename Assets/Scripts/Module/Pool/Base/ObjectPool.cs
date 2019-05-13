using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Module.ObjectPool
{
    public struct ObjectPoolData
    {
        /// <summary>
        /// 要使用对象池管理的对象
        /// </summary>
        public object Obj { get; set; }
        /// <summary>
        /// 对象的标记名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否自动销毁标志位
        /// </summary>
        public bool IsAutoDestroy { get; set; }
        /// <summary>
        /// 自动销毁间隔时间
        /// </summary>
        public float DestroyIntervalTime { get; set; }
        /// <summary>
        /// 活跃对象数量限制
        /// </summary>
        public int ActiveLimitNum { get; set; }

        public ObjectPoolData(object obj, string name, bool isAutoDestroy = true, float destroyIntervalTime = 1, int activeLimitNum = 3)
        {
            Obj = obj;
            Name = name;
            IsAutoDestroy = isAutoDestroy;
            DestroyIntervalTime = destroyIntervalTime;
            ActiveLimitNum = activeLimitNum;
        }
    }

    public abstract class ObjectPoolBase<T> where T : class 
    {
        public Dictionary<string, SubObjectPoolBase<T>> _poolDic;

        public ObjectPoolBase()
        {
            _poolDic = new Dictionary<string, SubObjectPoolBase<T>>();
        }

        protected abstract SubObjectPoolBase<T> GetNewSubPool(ObjectPoolData dat);

        /// <summary>
        /// 添加池，返回值为是否添加成功
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool AddSubPool(ObjectPoolData data)
        {
            if (!_poolDic.ContainsKey(data.Name))
            {
                _poolDic[data.Name] = GetNewSubPool(data);
                return true;
            }

            return false;
        }

        public bool RemoveSubPool<TName>(TName name)
        {
            string nameTemp = name.ToString();
            if (_poolDic.ContainsKey(nameTemp))
            {
                return _poolDic.Remove(nameTemp);
            }

            return true;
        }

        public T Spwan<TName>(TName name)
        {
            string nameTemp = name.ToString();
            if (_poolDic.ContainsKey(nameTemp))
            {
                return _poolDic[nameTemp].Spwan();
            }

            return null;
        }

        public bool Despwan<TName>(TName name, T obj)
        {
            string nameTemp = name.ToString();
            if (_poolDic.ContainsKey(nameTemp))
            {
                return _poolDic[nameTemp].Despwan(obj); ;
            }
            return false;
        }

        public bool DespwanAll<TName>(TName name)
        {
            string nameTemp = name.ToString();
            if (_poolDic.ContainsKey(nameTemp))
            {
                return _poolDic[nameTemp].DespwanAll();
            }
            return false;
        }
    }

    public abstract class SubObjectPoolBase<T> where T : class 
    {
        public string Name { get; private set; }
        private T _obj;
        private List<T> _activeList;
        private List<T> _inactiveList;
        protected ObjectPoolData _data;

        public SubObjectPoolBase(ObjectPoolData data)
        {
            _data = data;
            _activeList = new List<T>();
            _inactiveList = new List<T>();
            AutoDestroy(data);
        }

        protected virtual void AddActiveItem(T item)
        {
            _activeList.Add(item);
        }

        protected virtual void AddInactiveItem(T item)
        {
            _inactiveList.Add(item);
        }

        private async void AutoDestroy(ObjectPoolData data)
        {
            if (!data.IsAutoDestroy)
                return;

            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(data.DestroyIntervalTime));
                AutoDestroy(100, data.ActiveLimitNum);
            }
        }
        //time参数的单位是毫秒
        private async void AutoDestroy(int time, int limitCount)
        {
            if (_inactiveList.Count > limitCount)
            {
                int times = _inactiveList.Count - limitCount;
                T temp = null;

                for (int i = 0; i < times; i++)
                {

                    temp = _inactiveList[_inactiveList.Count - 1];
                    _inactiveList.Remove(temp);
                    DestroyItem(temp);
                    await Task.Delay(time);
                }
            }
        }
        /// <summary>
        /// 销毁子项
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void DestroyItem(T obj);
        /// <summary>
        /// 创建新的子项
        /// </summary>
        /// <returns></returns>
        protected abstract T SpawnNew();

        public virtual T Spwan()
        {
            T temp = null;
            if (_inactiveList.Count > 0)
            {
                temp = _inactiveList[0];
                _inactiveList.Remove(temp);
            }
            else
            {
                temp = SpawnNew();
            }

            AddActiveItem(temp);
            return temp;
        }

        public bool Despwan(T obj)
        {
            if (_activeList.Count > 0 && _activeList.Contains(obj))
            {
                FromActiveToInactive(obj);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DespwanAll()
        {
            int count = _activeList.Count;
            T temp = null;

            for (int i = 0; i < count; i++)
            {
                temp = _inactiveList[0];
                FromActiveToInactive(temp);
                return true;
            }

            return false;
        }

        protected virtual void FromActiveToInactive(T obj)
        {
            _activeList.Remove(obj);
            AddInactiveItem(obj);
        }
    }
}
