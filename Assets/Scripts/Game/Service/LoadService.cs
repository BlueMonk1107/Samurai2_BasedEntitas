using Manager;
using Manager.Parent;
using UnityEngine;
using Util;

namespace Game.Service
{
    /// <summary>
    /// 加载服务接口
    /// </summary>
    public interface ILoadService : ILoad,IInitService
    {
        /// <summary>
        /// 加载玩家预制
        /// </summary>
        void LoadPlayer();
    }

    public class LoadService : ILoadService
    {
        private GameParentManager _parentManager;


        public void Init(Contexts contexts)
        {
            contexts.service.SetGameServiceLoadService(this);
        }

        public int GetPriority()
        {
            return 0;
        }

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

        public void LoadPlayer()
        {
            var player = LoadAndInstaniate(Path.PLAYER_PREFAB, _parentManager.GetParnetTrans(ParentName.PlayerRoot));
            IView view = player.AddComponent<PlayerView>();
            IPlayerBehaviour behaviour = new PlayerBehaviour(player.transform,ModelManager.Single.PlayerData);
            IPlayerAni ani = null;
            Animator animator = player.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("玩家预制上为发现动画组件");
            }
            else
            {
                ani = new PlayerAni(animator);
            }
            var entity = Contexts.sharedInstance.game.CreateEntity();
            entity.AddGamePlayer(view, behaviour, ani);
            entity.AddGamePlayerAniState(PlayerAniIndex.IDLE);
            view.Init(Contexts.sharedInstance, entity);
        }
        
    }
}
