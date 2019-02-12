using Entitas;
using System.Collections.Generic;
using Game.Service;

namespace Game
{
    /// <summary>
    /// 判断技能按钮输入的是否有效
    /// </summary>
    public class InputJudgeHumanSkillSystem : ReactiveSystem<InputEntity>,IInitializeSystem
    {
        protected Contexts _contexts;

        public InputJudgeHumanSkillSystem(Contexts context) : base(context.input)
        {
            _contexts = context;
        }

        public void Initialize()
        {
            _contexts.input.ReplaceGameInputValidHumanSkill(false, 0);
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.GameInputButton);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.gameInputButton.InputButton == InputButton.ATTACK_X
                || entity.gameInputButton.InputButton == InputButton.ATTACK_O;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (InputEntity entity in entities)
            {
                ITimerService timerService = _contexts.service.gameServiceTimerService.TimerService;
                var timer = timerService.CreateTimer(TimerId.JUDGE_SKILL_TIMER, 0.2f, false);
                
                if (timer == null)
                {
                    timer = timerService.ResetTimerData(TimerId.JUDGE_SKILL_TIMER, 0.2f, false);
                    timer.AddCompleteListener(() => SetValid(entity,true));
                }
                else
                {
                    timer.AddCompleteListener(() => SetValid(entity, true));
                }

                SetValid(entity, false);
            }
           
        }

        private void SetValid(InputEntity entity,bool isValid)
        {
            var skillComponent = _contexts.input.gameInputValidHumanSkill;
            ReplaceValidHumanSkill(entity, isValid, skillComponent);
        }

        private void ReplaceValidHumanSkill(InputEntity entity, bool isValid,InputValidHumanSkill skill)
        {
            int code = 0;
            if (skill != null)
            {
                code = skill.SkillCode;
            }

            if (!isValid)
            {
                code = _contexts.service
                    .gameServiceSkillCodeService.SkillCodeService
                    .GetCurrentSkillCode(entity.gameInputButton.InputButton, code);
            }
            
            _contexts.input.ReplaceGameInputValidHumanSkill(isValid, code);
        }
    }
}
