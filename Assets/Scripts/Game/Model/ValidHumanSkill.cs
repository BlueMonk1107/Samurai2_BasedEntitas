using UnityEngine;

namespace Game.Model
{
    /// <summary>
    /// 有效的技能数据
    /// </summary>
    public class ValidHumanSkill      
    {
        public ValidHumanSkill(int code)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }
}
