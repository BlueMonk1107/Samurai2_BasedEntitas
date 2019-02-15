using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Game.Model
{
    [Game,Unique]
    public class HumanSkillConfig : IComponent
    {
        public List<ValidHumanSkill> ValidHumanSkills;
        /// <summary>
        /// 当前有效编码的最大长度
        /// </summary>
        public int LengthMax;
    }
}
