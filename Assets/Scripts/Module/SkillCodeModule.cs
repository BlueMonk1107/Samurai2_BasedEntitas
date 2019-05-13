using Game;
using UnityEngine;

namespace Module.Skill
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
                currentCode = currentCode * 10 + code;
            }

            return currentCode;
        }

        /// <summary>
        /// 获取int类型的技能编码
        /// </summary>
        /// <param name="skillName"></param>
        /// <param name="prefix"></param>
        /// <param name="posfix"></param>
        /// <returns></returns>
        public int GetSkillCode(string skillName,string prefix,string posfix)
        {
            if (skillName.Contains("X") || skillName.Contains("O"))
            {
                string codeString = skillName;
                if (!string.IsNullOrEmpty(prefix))
                {
                    codeString = skillName.Remove(0, prefix.Length);
                }

                if (!string.IsNullOrEmpty(posfix))
                {
                    codeString = skillName.Remove(skillName.Length - posfix.Length, posfix.Length);
                }

                return ConvertStringToInt(codeString);
            }

            return -1;
        }

        /// <summary>
        /// 获取XXOO类型的技能编码
        /// </summary>
        /// <returns></returns>
        public string GetCodeString(int code)
        {
            return ConvertIntToString(code);
        }

        //转换string编码到int 从xxoo类型转换成int类型编码
        private int ConvertStringToInt(string codeString)
        {
            int[] codes = new int[codeString.Length];
            char[] chars = codeString.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == SkillButton.O.ToString()[0])
                {
                    codes[i] = (int) SkillButton.O;
                }
                else if (chars[i] == SkillButton.X.ToString()[0])
                {
                    codes[i] = (int)SkillButton.X;
                }
            }

            int code = 0;

            for (int i = 0; i < codes.Length; i++)
            {
                code += (int) (codes[i]*Mathf.Pow(10, codes.Length - 1 - i));
            }

            return code;
        }
        //转换int编码到string 转换成xxoo类型的编码
        private string ConvertIntToString(int code)
        {
            string codeString = code.ToString();
            string[] codeStrings = new string[codeString.Length];

            for (int i = 0; i < codeStrings.Length; i++)
            {
                if (int.Parse(codeString[i].ToString()) == (int) SkillButton.O)
                {
                    codeStrings[i] = SkillButton.O.ToString();
                }
                else if(int.Parse(codeString[i].ToString()) == (int)SkillButton.X)
                {
                    codeStrings[i] = SkillButton.X.ToString();
                }
            }

            codeString = string.Join("", codeStrings);

            return codeString;
        }
    }

    public enum SkillButton
    {
        X = 1,
        O = 2
    }
}
