using Entitas;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private Systems _systems;

        public void Start()
        {
            var services = new Services(
                new FindObjectService(),
                new EntitasInputService(),
                new UnityInputService(),
                new LogService());

            _systems = new InitFeature(Contexts.sharedInstance, services);

            _systems.Initialize();
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
