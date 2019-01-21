using Game.Service;
using UnityEngine;

namespace Game
{
    public class ServiceFeature : Feature     
    {
        public ServiceFeature(Contexts contexts, ServiceManager services) : base("InitService")
        {
            Add(new InitServicesSystem(contexts, services));
            Add(new ExcuteServicesSystem(contexts, services));
        }
    }
}
