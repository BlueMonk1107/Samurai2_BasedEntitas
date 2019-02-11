

using System.Collections.Generic;
using System.Linq;
using Manager.Parent;
using Module.Timer;
using UnityEngine;

namespace Game.Service
{
    /// <summary>
    /// 服务管理类基础接口
    /// </summary>
    public interface IServiceManager : IInitService, IExecuteService
    {

    }

    public class ServiceManager : IServiceManager
    {
        private Dictionary<int, HashSet<IInitService>> _initServices;
        private HashSet<IExecuteService> _executeServices;

        public ServiceManager(GameParentManager parentManager)
        {
            _initServices = new Dictionary<int, HashSet<IInitService>>();
            _executeServices = new HashSet<IExecuteService>();

            IInitService[] services = InitServices(parentManager);

            AddInitServices(services);
            AddExecuteServices(services);

            var result = from temp in _initServices orderby temp.Key select temp;
            _initServices = result.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// 初始化服务对象数组方法
        /// </summary>
        /// <param name="parentManager"></param>
        /// <returns></returns>
        private IInitService[] InitServices(GameParentManager parentManager)
        {
            IInitService[] services =
            {
                new SkillCodeService(),                new FindObjectService(),
                new EntitasInputService(),
                new LogService(),
                new LoadService(parentManager),
                new TimerService(new TimerManager()),
                new UnityInputService()
            };

            return services;
        }

        public void Init(Contexts contexts)
        {
            foreach (var service in _initServices)
            {
                foreach (IInitService initService in service.Value)
                {
                    initService.Init(contexts);
                }
            }
        }

        public int GetPriority()
        {
            throw new System.NotImplementedException();
        }

        public void Excute()
        {
            foreach (IExecuteService service in _executeServices)
            {
                service.Excute();
            }
        }

        private void AddInitServices(IInitService[] services)
        {
            foreach (var service in services)
            {
                AddInitService(service.GetPriority(),service);
            }
        }

        private void AddExecuteServices(IInitService[] services)
        {
            foreach (var service in services)
            {
                IExecuteService temp = service as IExecuteService;
                if (temp != null)
                {
                    AddExecuteService(temp);
                }
            }
        }

        /// <summary>
        /// 添加初始化服务对象 第一个参数为优先级：0开始
        /// </summary>
        /// <param name="service"></param>
        private void AddInitService(int priority, IInitService service)
        {
            if (priority < 0)
            {
                Debug.LogError("优先级从0开始，不能为负");
                return;
            }
            if (!_initServices.ContainsKey(priority))
            {
                _initServices[priority] = new HashSet<IInitService>();
            }

            _initServices[priority].Add(service);
        }

        /// <summary>
        /// 添加帧函数服务对象
        /// </summary>
        /// <param name="service"></param>
        private void AddExecuteService(IExecuteService service)
        {
            _executeServices.Add(service);
        }

    }
}
