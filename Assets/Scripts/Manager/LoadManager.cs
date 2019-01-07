using System;
using UIFrame;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager
{
    public class LoadManager : SingletonBase<LoadManager>
    {
        public T Load<T>(string path, string name) where T : class
        {
            return Resources.Load(path + name) as T;
        }

        public GameObject LoadAndInstaniate(string path,Transform parnet)
        {
            var temp = Resources.Load<GameObject>(path);

            if (temp == null)
            {
                Debug.LogError("path :" + path + " is null");
                return null;
            }
            else
            {
                var go = Object.Instantiate(temp, parnet);
                return go;
            }
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}
