using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 查找服务组件
    /// </summary>
    [Game,Unique]
    public class FindObjectServiceComponent : IComponent
    {
        public IFindObjectService FindObjectService;
    }

    /// <summary>
    /// 输入服务组件
    /// </summary>
    [Game, Unique]
    public class InputServiceComponent : IComponent
    {
        public IInputService InputService;
    }
}
