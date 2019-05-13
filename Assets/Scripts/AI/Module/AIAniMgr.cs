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
        private Dictionary<string, AniControl> _specialAniDic;

        public AIAniMgr(object selfTrans)
        {
            _specialAniDic = new Dictionary<string, AniControl>();

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
                return GetAniClip(name).length;
            }
            
        }

        private AniControl GetAniControl(string name)
        {
            if (!_specialAniDic.ContainsKey(name))
            {
                _specialAniDic[name] = InitSpecial(Path.ENEMY_PATH + name);
            }

            return _specialAniDic[name];
        }

        private AnimationClip GetAniClip(string name)
        {
            if (_ani[name] != null)
            {
                return _ani[name].clip;
            }
            else
            {
                return GetAniControl(name).GetClip();
            }
        }

        private AniControl InitSpecial(string path)
        {
            AniControl control = null;
            GameObject deadPrefab = LoadManager.Single.Load<GameObject>(path, "");
            GameObject dead = GameObject.Instantiate(deadPrefab);
            if (dead != null)
            {
                control = dead.AddComponent<AniControl>();
                control.Init(_self.position);
            }
            else
            {
               DebugMsg.LogError("动画预制未找到，路径为：" + path);
            }

            return control;
        }
    }
}
