using Entitas;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 初始化服务系统
    /// </summary>
    public class InitServicesSystem : IInitializeSystem
    {
        private Contexts _contexts;
        private Services _services;

        public InitServicesSystem(Contexts contexts,Services services)
        {
            _contexts = contexts;
            _services = services;
        }

        public void Initialize()
        {
            InitUniqueComponents(_contexts, _services);

            InitServices(_contexts, _services);
        }

        private void InitServices(Contexts contexts, Services service)
        {
            service.EntitasInputService.Init(contexts);
            service.UnityInputService.Init(contexts);
        }

        /// <summary>
        /// 初始化单例组件
        /// </summary>
        /// <param name="contexts"></param>
        /// <param name="services"></param>
        private void InitUniqueComponents(Contexts contexts, Services services)
        {
            contexts.game.SetGameFindObjectService(services.FindObjectService);
            contexts.game.SetGameEntitasInputService(services.EntitasInputService);
            contexts.game.SetGameLogService(services.LogService);
            contexts.game.SetGameLoadService(services.LoadService);
        }
    }
}
