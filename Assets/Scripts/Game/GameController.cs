using Entitas;
using Game.Service;
using Game.View;
using Manager;
using Manager.Parent;
using Model;
using Module;
using UnityEngine;
using Util;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private Systems _systems;
        private Contexts _contexts;
        private IServiceManager _serviceManager;

        public void Awake()
        {
            _contexts = Contexts.sharedInstance;
            InitManager();

            _systems = new InitFeature(Contexts.sharedInstance);

            _systems.Initialize();
           
            _contexts.game.SetGameGameState(GameState.START);
        }

        private void InitManager()
        {
            var parentManager = transform.GetOrAddComponent<GameParentManager>();
            parentManager.Init();

            InitCameraController(parentManager);

            ModelManager.Single.Init();

            _serviceManager = new ServiceManager(parentManager);
            _serviceManager.Init(_contexts);

            InitUIController(parentManager);

            LoadAudioManager.Single.Init();

            InitPoolMgr();
        }

        private void InitCameraController(GameParentManager parentManager)
        {
            var cameraContrller = parentManager.GetParnetTrans(ParentName.CameraController);
            CameraController cameraController = cameraContrller.gameObject.AddComponent<CameraController>();
            var entity = _contexts.game.CreateEntity();
            entity.AddGameCameraState(CameraAniName.NULL);
            cameraController.Init(_contexts, entity);
        }

        private void InitUIController(GameParentManager parentManager)
        {
            var uiParnet = parentManager.GetParnetTrans(ParentName.UIController);
            UIController uiCopntroller = uiParnet.gameObject.AddComponent<UIController>();
            uiCopntroller.Init();
        }

        private void InitPoolMgr()
        {
            GameObject poolMgr = new GameObject("PoolMgr");
            poolMgr.transform.SetParent(transform);
            poolMgr.AddComponent<PoolManager>();
        }

        private void Update()
        {
            _serviceManager.Excute();
            _systems.Execute();
            _systems.Cleanup();
        }

        private void OnDestroy()
        {
            _systems.TearDown();
        }
    }
}
