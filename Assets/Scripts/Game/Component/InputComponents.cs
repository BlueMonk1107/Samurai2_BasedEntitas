using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 输入按键
    /// </summary>
    [Input, Unique]
    public class InputButtonComponent : IComponent
    {
        public InputButton InputButton;
        public InputState InputState;
    }

    [Input, Unique]
    public class InputValidHumanSkill : IComponent
    {
        /// <summary>
        /// 标记连续按键是否有效
        /// </summary>
        public bool IsValid;

        public int SkillCode;
    }
}
