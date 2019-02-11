using Game;
using UnityEngine;

namespace Module
{
    /// <summary>
    /// 人物技能编码功能模块
    /// </summary>
    public class SkillCodeModule     
    {
        public int GetCurrentSkillCode(SkillButton button, int currentCode)
        {
            int code = (int) button;
            if (currentCode < 0)
            {
                Debug.LogError("SkillCode不能小于0");
            }
            else if (currentCode == 0)
            {
                currentCode = code;
            }
            else
            {
                currentCode = (int)(code*Mathf.Pow(10, GetCodeLength(currentCode))) + currentCode;
            }

            return currentCode;
        }

        //获取当前编码是几位数
        private int GetCodeLength(int currentCode)
        {
            return currentCode.ToString().Length;
        }
    }

    public enum SkillButton
    {
        ATTACK_X = 1,
        ATTACK_O = 2
    }
}
