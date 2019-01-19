using Entitas;
using Manager;
using Manager.Parent;
using Model;
using UnityEngine;
using Util;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private Systems _systems;
        private GameParentManager _parentManager;
        private Contexts _contexts;

        public void Start()
        {
            _contexts = Contexts.sharedInstance;
            InitManager();
            var services = new Services(
                new FindObjectService(),
                new EntitasInputService(),
                new UnityInputService(),
                new LogService(),
                new LoadService(_parentManager));

            _systems = new InitFeature(Contexts.sharedInstance, services);

            _systems.Initialize();
           
            _contexts.game.SetGameGameState(GameState.START);
        }

        private void InitManager()
        {
            _parentManager = transform.GetOrAddComponent<GameParentManager>();
            _parentManager.Init();

            var cameraContrller = _parentManager.GetParnetTrans(ParentName.CameraController);
            CameraController cameraController = cameraContrller.gameObject.AddComponent<CameraController>();
            var entity = _contexts.game.CreateEntity();
            entity.AddGameCameraState(CameraAniName.NULL);
            cameraController.Init(_contexts, entity);

            ModelManager.Single.Init();
        }

        private void Update()
        {
            _systems.Execute();
            _systems.Cleanup();
        }

        private void OnDestroy()
        {
            _systems.TearDown();
        }
    }
}
