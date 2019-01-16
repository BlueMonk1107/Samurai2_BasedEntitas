using UnityEngine;

namespace Game
{
    /// <summary>
    /// 系统初始化部分
    /// </summary>
    public class InitFeature : Feature     
    {
        public InitFeature(Contexts contexts, Services services) : base("Init")
        {
            Add(new GameEventSystems(contexts));
            Add(new ServiceFeature(contexts, services));
            Add(new ViewFeature(contexts));
            Add(new SystemFeature(contexts));
        }
    }
}
