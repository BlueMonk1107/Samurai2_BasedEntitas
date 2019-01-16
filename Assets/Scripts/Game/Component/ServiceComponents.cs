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
    public class EntitasInputServiceComponent : IComponent
    {
        public IInputService EntitasInputService;
    }

    /// <summary>
    /// 输入服务组件
    /// </summary>
    [Game, Unique]
    public class LogServiceComponent : IComponent
    {
        public ILogService LogService;
    }
}
