using Manager;
using UnityEngine;
using Util;

namespace Game.AI.ViewEffect
{
    public class EffectMgr    
    {
        public GameObject Play<T>(T key,Vector3 position)
        {
            var go = PoolManager.Single.EffectPool.Spwan(key);
            if (go != null)
            {
                go.transform.position = position;
                go.transform.GetOrAddComponent<EffectControl>().Play();
            }
            else
            {
                Debug.LogError("内存池内获取到的"+key+"对象为空");
            }
           
            return go.gameObject;
        }
    }
}
