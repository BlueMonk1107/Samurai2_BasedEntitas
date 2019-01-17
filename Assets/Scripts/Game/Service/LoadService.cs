using Manager;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ╪сть╫с©з
    /// </summary>
    public interface ILoadService : ILoad
    {
    }

    public class LoadService : ILoadService
    {
        public T Load<T>(string path, string name) where T : class
        {
            return LoadManager.Single.Load<T>(path, name);
        }

        public GameObject LoadAndInstaniate(string path, Transform parnet)
        {
            return LoadManager.Single.LoadAndInstaniate(path, parnet);
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return LoadManager.Single.LoadAll<T>(path);
        }
    }
}
