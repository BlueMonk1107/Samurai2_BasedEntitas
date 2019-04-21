using System;
using Game.View;
using Manager;
using Manager.Parent;
using UnityEngine;
using Util;
using Object = UnityEngine.Object;

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

        void LoadEnemy(string enemyName, Transform parent);
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

            player.AddComponent<IgnorForce>();
            player.AddComponent<PlayerCollider>();

            IView view = player.AddComponent<PlayerView>();
            IPlayerBehaviour behaviour = new PlayerBehaviour(player.transform,ModelManager.Single.PlayerData);
            IPlayerAni ani = null;
            IPlayerAudio audio = new PlayerAudio(player.GetComponentInChildren<AudioSource>());

            Animator animator = player.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("玩家预制上未发现动画组件");
            }
            else
            {
                ani = new PlayerAni(animator, new CustomAniEventManager(animator));
            }
            var entity = Contexts.sharedInstance.game.CreateEntity();
            entity.AddGamePlayer(view, behaviour, ani, audio);
            entity.AddGamePlayerAniState(PlayerAniIndex.IDLE);
            view.Init(Contexts.sharedInstance, entity);

            LoadTrails(player.transform, animator);
        }

        private void LoadTrails(Transform player, Animator animator)
        {
            var trails = LoadAndInstaniate(Path.TRAILS_COMBO_PREFAB, player);
            var manager = trails.transform.GetOrAddComponent<TrailComboManager>();
            var entity = Contexts.sharedInstance.game.CreateEntity();
            manager.Init(Contexts.sharedInstance, entity, animator);
        }

        public void LoadEnemy(string enemyName,Transform parent)
        {
            var enemy = LoadAndInstaniate(Path.ENEMY_PATH + enemyName, parent);
            string scriptName = Consts.VIEW_NAMESPACE + "." + enemyName + Consts.VIEW_POSTFIX;
            Type viewType = Type.GetType(scriptName);
            IView view = null;
            if (viewType != null)
            {
                view = enemy.AddComponent(viewType) as IView;
            }
            else
            {
                Debug.LogError("未找到类，名称为"+ scriptName);
                return;
            }

            var entity = Contexts.sharedInstance.game.CreateEntity();
            view.Init(Contexts.sharedInstance, entity);
        }
        
    }
}
