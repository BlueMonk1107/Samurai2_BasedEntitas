using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class HumanSkillModel
    {
        public List<SkillModel> Skills;
    }

    [Serializable]
    public class SkillModel
    {
        public string Code;
        public int Level;
    }
}
