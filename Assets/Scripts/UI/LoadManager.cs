using UnityEngine;

namespace UIFrame
{
    public class LoadManager      
    {
        public static LoadManager Instacne { get; private set; } = new LoadManager();

        public T Load<T>(string path,string name) where T:class 
        {
            return Resources.Load(path + name) as T;
        }
    }
}
