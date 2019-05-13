using System;
using System.Collections.Generic;
using BlueGOAP;
using Manager;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AIAniMgr
    {
        private Animation _ani;
        private Transform _self;
        private Dictionary<string, GameObject> _specialAniPrefabDic;

        public AIAniMgr(object selfTrans)
        {
            _specialAniPrefabDic = new Dictionary<string, GameObject>();

            try
            {
                _self = selfTrans as Transform;
                _ani = _self.GetComponent<Animation>();
            }
            catch (Exception)
            {
                Debug.LogError("未获取到当前对象的Transform");
            }

            if (_ani == null)
            {
                Debug.LogError("未获取到当前对象的动画组件");
            }
           
        }

        public void Play<T>(T aniName)
        {
            string name = aniName.ToString();

            if (_ani[name] != null)
            {
                _ani.CrossFade(name);
            }
            else
            {
                GetAniControl(name).Play();
            }
        }

        public float GetAniLength<T>(T aniName)
        {
            string name = aniName.ToString();
            if (_ani[name] != null)
            {
                return _ani[name].length;
            }
            else
            {
                return GetSpecialClipLength(name);
            }
            
        }

        private float GetSpecialClipLength(string name)
        {
            InitSpecialAniDIc(name);
            return _specialAniPrefabDic[name].GetComponent<Animation>().clip.length;
        }

        private SpecialDeadAniControl GetAniControl(string name)
        {
            InitSpecialAniDIc(name);
            return InitSpecial(_specialAniPrefabDic[name]); 
        }

        private void InitSpecialAniDIc(string name)
        {
            if (!_specialAniPrefabDic.ContainsKey(name))
            {
                _specialAniPrefabDic[name] = LoadManager.Single.Load<GameObject>(Path.ENEMY_PATH + name, "");
            }
        }

        private SpecialDeadAniControl InitSpecial(GameObject prefab)
        {
            SpecialDeadAniControl control = null;
            GameObject dead = GameObject.Instantiate(prefab);
            if (dead != null)
            {
                control = dead.AddComponent<SpecialDeadAniControl>();
                control.Init(_self.position);
            }
            else
            {
               DebugMsg.LogError("动画预制未找到");
            }

            return control;
        }
    }
}
