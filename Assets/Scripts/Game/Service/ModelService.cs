using System.Collections.Generic;
using Game.Model;
using Manager;
using Model;
using Module;
using Module.Skill;

namespace Game.Service
{
    /// <summary>
    /// 数据服务
    /// </summary>
    public interface IModelService : IInitService   
    {
    }

    public interface IConfigModelService : IModelService
    {
        
    }

    /// <summary>
    /// 配置数据服务
    /// </summary>
    public class ConfigModelService : IConfigModelService
    {
        public void Init(Contexts contexts)
        {
            InitHumanSkillCondig(contexts);
        }
        public int GetPriority()         
        {
            return 0;
        }

        private void InitHumanSkillCondig(Contexts contexts)
        {
            List<ValidHumanSkill> skills = GetValidList();
            int length = GetLengthMaxItem(skills);
            contexts.game.SetGameModelHumanSkillConfig(skills,length);
        }

        private List<ValidHumanSkill> GetValidList()
        {
            SkillCodeModule skillCode = new SkillCodeModule();
            List<ValidHumanSkill> skills = new List<ValidHumanSkill>();
            int codeTemp = 0;

            foreach (SkillModel model in ModelManager.Single.HumanSkillModel.Skills)
            {
                if (model.Level <= (int)DataManager.Single.LevelIndex)
                {
                    codeTemp = skillCode.GetSkillCode(model.Code, "", "");
                    skills.Add(new ValidHumanSkill(codeTemp));
                }
            }

            return skills;
        }

        private int GetLengthMaxItem(List<ValidHumanSkill> skills)
        {
            int length = 0;
            foreach (ValidHumanSkill skill in skills)
            {
                if (skill.Code.ToString().Length > length)
                {
                    length = skill.Code.ToString().Length;
                }
            }

            return length;
        }
    }
}
