using Manager;
using Manager.Parent;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 加载服务接口
    /// </summary>
    public interface ILoadService : ILoad
    {
        /// <summary>
        /// 加载玩家预制
        /// </summary>
        IPlayerBehaviour LoadPlayer();
    }

    public class LoadService : ILoadService
    {
        private GameParentManager _parentManager;
        public LoadService(GameParentManager parentManager)
        {
            _parentManager = parentManager;
        }

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

        public IPlayerBehaviour LoadPlayer()
        {
            var player = LoadAndInstaniate(Path.PLAYER_PREFAB, _parentManager.GetParnetTrans(ParentName.PlayerRoot));
            PlayerView view = player.AddComponent<PlayerView>();
            view.Init();
            return view;
        }
    }
}
