
using System;
using System.Collections.Generic;

namespace BlueGOAP
{
    public class ObjectPool
    {
        private static ObjectPool _instance;
        public static ObjectPool Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new ObjectPool();

                return _instance;
            }
        }

        private Dictionary<Type, List<object>> _activeDic = new Dictionary<Type, List<object>>();
        private Dictionary<Type, List<object>> _inactiveDic = new Dictionary<Type, List<object>>();

        public T Spwan<T>(params object[] args) where T : class 
        {
            Type type = typeof(T);
            object temp = null;

            if (_inactiveDic.ContainsKey(type))
            {
                if (_inactiveDic[type].Count > 0)
                {
                    temp = _inactiveDic[type][0];
                    _inactiveDic[type].Remove(temp);
                }
            }
            else
            {
                _inactiveDic[type] = new List<object>();
            }

            temp = SpwanNew(type, args);
           
            if(!_activeDic.ContainsKey(type))
                _activeDic[type] = new List<object>();

            _activeDic[type].Add(temp);
            return temp as T;
        }

        private object SpwanNew(Type type,params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public void Despawn<T>(T obj)
        {
            Type type = typeof(T);
            if (_activeDic.ContainsKey(type))
            {
                if(_activeDic[type].Contains(obj))
                {
                    _activeDic[type].Remove(obj);
                    _inactiveDic[type].Add(obj);
                }
                else
                {
                    DebugMsg.LogError(type + "此对象不在当前活跃对象缓存中");
                }
            }
            else
            {
                DebugMsg.LogError(type+"此类型不在当前活跃对象缓存中");
            }
        }
    }
}
