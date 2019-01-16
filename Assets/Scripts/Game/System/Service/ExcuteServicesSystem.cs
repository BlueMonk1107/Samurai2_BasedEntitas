using Entitas;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 调用服务部分的帧函数
    /// </summary>
    public class ExcuteServicesSystem : IExecuteSystem     
    {
        private Contexts _contexts;
        private Services _services;

        public ExcuteServicesSystem(Contexts contexts, Services services)        
        {
            _contexts = contexts;
            _services = services;
        }

        public void Execute()
        {
            _services.UnityInputService.Update();
        }
    }
}
