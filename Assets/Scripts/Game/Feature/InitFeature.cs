using Game.Service;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 系统初始化部分
    /// </summary>
    public class InitFeature : Feature     
    {
        public InitFeature(Contexts contexts) : base("Init")
        {
            Add(new GameEventSystems(contexts));

            Add(new ViewFeature(contexts));
            Add(new InputFeature(contexts));
            Add(new GameFeature(contexts));
        }
    }
}
