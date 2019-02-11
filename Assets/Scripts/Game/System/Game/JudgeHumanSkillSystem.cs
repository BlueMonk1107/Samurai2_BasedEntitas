using Entitas;
using System.Collections.Generic;
namespace Game
{
    public class InputJudgeHumanSkillSystem : ReactiveSystem<InputEntity>     
    {
         protected Contexts _contexts;

        public InputJudgeHumanSkillSystem(Contexts context)  : base(context.input)        
        {
             _contexts = context;
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
        }
    }
}
