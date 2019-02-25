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

        public void Start()
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

            var cameraContrller = parentManager.GetParnetTrans(ParentName.CameraController);
            CameraController cameraController = cameraContrller.gameObject.AddComponent<CameraController>();
            var entity = _contexts.game.CreateEntity();
            entity.AddGameCameraState(CameraAniName.NULL);
            cameraController.Init(_contexts, entity);

            ModelManager.Single.Init();

            _serviceManager = new ServiceManager(parentManager);
            _serviceManager.Init(_contexts);

            var uiParnet = parentManager.GetParnetTrans(ParentName.UIController);
            UIController uiCopntroller = uiParnet.gameObject.AddComponent<UIController>();
            uiCopntroller.Init();

            LoadAudioManager.Single.Init();
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
