using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ÊäÈë°´¼ü
    /// </summary>
    [Input, Unique]
    public class InputButtonComponent : IComponent
    {
        public InputButton InputButton;
    }
}
