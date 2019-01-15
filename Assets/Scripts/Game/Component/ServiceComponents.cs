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
}
