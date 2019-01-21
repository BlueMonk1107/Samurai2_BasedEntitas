

using System.Collections.Generic;
using Manager.Parent;

namespace Game.Service
{
    /// <summary>
    /// 服务管理类基础接口
    /// </summary>
    public interface IServiceManager : IInitService,IExecuteService
    {
        
    }

    public class ServiceManager : IServiceManager
    {
        private HashSet<IInitService> _initServices;
        private HashSet<IExecuteService> _executeServices;

        public ServiceManager(GameParentManager parentManager)
        {
            _initServices = new HashSet<IInitService>();
            _executeServices = new HashSet<IExecuteService>();

            AddInitServices(this, parentManager);
            AddExecuteServices(this);
        }

        public void Init(Contexts contexts)
        {
            foreach (IInitService service in _initServices)
            {
                service.Init(contexts);
            }
        }

        public void Excute()
        {
            foreach (IExecuteService service in _executeServices)
            {
                service.Excute();
            }
        }

        private void AddInitServices(ServiceManager services, GameParentManager parentManager)
        {
            services.AddInitService(new FindObjectService());
            services.AddInitService(new EntitasInputService());
            services.AddInitService(new UnityInputService());
            services.AddInitService(new LogService());
            services.AddInitService(new LoadService(parentManager));
            services.AddInitService(new TimerService());
        }

        private void AddExecuteServices(ServiceManager services)
        {
            services.AddExecuteService(new UnityInputService());
            services.AddExecuteService(new TimerService());
        }

        /// <summary>
        /// 添加初始化服务对象
        /// </summary>
        /// <param name="service"></param>
        public void AddInitService(IInitService service)
        {
            _initServices.Add(service);
        }

        /// <summary>
        /// 添加帧函数服务对象
        /// </summary>
        /// <param name="service"></param>
        public void AddExecuteService(IExecuteService service)
        {
            _executeServices.Add(service);
        }
       
    }
}
