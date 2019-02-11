using Entitas;
using System.Collections.Generic;
namespace Game
{
    /// <summary>
    /// 有效技能响应系统
    /// </summary>
    public class InputInputValidHumanSkillSystem : ReactiveSystem<InputEntity>     
    {
         protected Contexts _contexts;

        public  InputInputValidHumanSkillSystem(Contexts context)  : base(context.input)        
        {
             _contexts = context;
        }
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)         
        {
            return context.CreateCollector(InputMatcher.GameInputValidHumanSkill);
        }
        protected override bool Filter(InputEntity entity)         
        {
            return entity.hasGameInputValidHumanSkill
                && entity.gameInputValidHumanSkill.IsValid;
        }
        protected override void Execute(List<InputEntity> entities)         
        {
            //发出信号，当前播放技能，动画，声音，特效
            //
            
            foreach (InputEntity entity in entities)
            {
                _contexts.service.gameServiceLogService.LogService.Log(entity.gameInputValidHumanSkill.SkillCode.ToString());
                entity.ReplaceGameInputValidHumanSkill(false,0);
            }
        }
    }
}
