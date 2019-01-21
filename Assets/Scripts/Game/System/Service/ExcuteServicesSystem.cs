using Entitas;
using Game.Service;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 调用服务部分的帧函数
    /// </summary>
    public class ExcuteServicesSystem : IExecuteSystem     
    {
        private Contexts _contexts;
        private ServiceManager _services;

        public ExcuteServicesSystem(Contexts contexts, ServiceManager services)        
        {
            _contexts = contexts;
            _services = services;
        }

        public void Execute()
        {
            //_services.UnityInputService.Update();
        }
    }
}
