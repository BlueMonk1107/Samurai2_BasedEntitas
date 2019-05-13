using Entitas;
using System.Collections.Generic;
using Game.Service;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 人物输入技能状态响应类
    /// </summary>
    public class InputHumanSkillStateSystem : ReactiveSystem<InputEntity>, IInitializeSystem
    {
        protected Contexts _contexts;

        public InputHumanSkillStateSystem(Contexts context) : base(context.input)
        {
            _contexts = context;
        }

        public void Initialize()
        {
            _contexts.input.ReplaceGameInputHumanSkillState(false, 0);
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

                var timer = timerService.CreatOrRestartTimer(TimerId.JUDGE_SKILL_TIMER, 0.2f, false);
                timer.AddCompleteListener(() => SetValid(entity, true));

                SetValid(entity, false);
            }

        }

        private void SetValid(InputEntity entity, bool isValid)
        {
            var skillComponent = _contexts.input.gameInputHumanSkillState;
            ReplaceValidHumanSkill(entity, isValid, skillComponent);
        }

        private void ReplaceValidHumanSkill(InputEntity entity, bool isValid, InputHumanSkillState skill)
        {
            int code = 0;
            if (skill != null)
            {
                code = skill.SkillCode;
            }

            if (!isValid)
            {
                isValid = JudgeLength(code);
            }

            if (!isValid)
            {
                code = _contexts.service
                   .gameServiceSkillCodeService.SkillCodeService
                   .GetCurrentSkillCode(entity.gameInputButton.InputButton, code);
            }

            if (isValid)
            {
                ITimerService timerService = _contexts.service.gameServiceTimerService.TimerService;
                timerService.StopTimer(timerService.GeTimer(TimerId.JUDGE_SKILL_TIMER),false);
            }

            _contexts.input.ReplaceGameInputHumanSkillState(isValid, code);
        }

        //对比编码长度，等于最大长度返回true
        private bool JudgeLength(int code)
        {
            int max = _contexts.game.gameModelHumanSkillConfig.LengthMax;

            return code.ToString().Length == max;
        }
    }
}
