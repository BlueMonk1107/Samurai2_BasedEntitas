using UnityEngine;

namespace Game
{
    public class ServiceFeature : Feature     
    {
        public ServiceFeature(Contexts contexts, Services services) : base("InitService")
        {
            Add(new InitServicesSystem(contexts, services));
            Add(new ExcuteServicesSystem(contexts, services));
        }
    }
}
