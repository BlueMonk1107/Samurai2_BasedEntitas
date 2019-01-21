using Entitas;
using Entitas.CodeGeneration.Attributes;
using Game.Service;
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

    /// <summary>
    /// 输入服务组件
    /// </summary>
    [Game, Unique]
    public class LoadServiceComponent : IComponent
    {
        public ILoadService LoadService;
    }

    [Game,Unique]
    public class TimerServiceComponent : IComponent
    {
        public ITimerService TimerService;
    }
}
