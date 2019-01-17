using Entitas;
using Manager.Parent;
using UnityEngine;
using Util;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private Systems _systems;
        private GameParentManager _parentManager;

        public void Start()
        {
            InitManager();
            var services = new Services(
                new FindObjectService(),
                new EntitasInputService(),
                new UnityInputService(),
                new LogService(),
                new LoadService(_parentManager));

            _systems = new InitFeature(Contexts.sharedInstance, services);

            _systems.Initialize();

            Contexts.sharedInstance.game.SetGameGameState(GameState.START);
        }

        private void InitManager()
        {
            _parentManager = transform.GetOrAddComponent<GameParentManager>();
            _parentManager.Init();

            var cameraContrller = _parentManager.GetParnetTrans(ParentName.CameraController);
            cameraContrller.gameObject.AddComponent<CameraController>();
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
