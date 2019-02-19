using Entitas;
using System.Collections.Generic;
using System.Linq;
using Game.Model;
using Game.Service;

namespace Game
{
    /// <summary>
    /// 判断技能按钮输入的是否有效
    /// </summary>
    public class InputJudgeHumanSkillSystem : ReactiveSystem<InputEntity>
    {
        protected Contexts _contexts;

        public InputJudgeHumanSkillSystem(Contexts context) : base(context.input)
        {
            _contexts = context;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.GameInputHumanSkillState);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasGameInputHumanSkillState
                && entity.gameInputHumanSkillState.IsEnd;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (InputEntity entity in entities)
            {
                int code = entity.gameInputHumanSkillState.SkillCode;
                code = GetValidCode(code);
                entity.ReplaceGameInputHumanSkillState(false, 0);
                _contexts.game.ReplaceGameValidHumanSkill(code);
            }
        }

        private int GetValidCode(int code)
        {
            if (JudgeIsVaildCode(code))
            {
                return code;
            }
            else
            {
                return GetLongValidCode(code);
            }
        }

        //获取错误编码中最长的有效编码
        private int GetLongValidCode(int code)
        {
            string codeString = code.ToString();
            codeString = codeString.Remove(codeString.Length - 1, 1);
            
            return GetValidCode(int.Parse(codeString));
        }

        private bool JudgeIsVaildCode(int code)
        {
            return GetValidData().Any(u => u.Code == code);
        }

        private List<ValidHumanSkill> GetValidData()
        {
            return _contexts.game.gameModelHumanSkillConfig.ValidHumanSkills;
        }

    }
}
