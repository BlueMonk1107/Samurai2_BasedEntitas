using Entitas;
using System.Collections.Generic;
namespace Game
{
    /// <summary>
    /// 人物技能响应系统，只接收有效的技能编码
    /// </summary>
    public class GamePlayHumanSkillSystem : ReactiveSystem<GameEntity>
    {
        protected Contexts _contexts;

        public GamePlayHumanSkillSystem(Contexts context) : base(context.game)
        {
            _contexts = context;
        }
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.GamePlayHumanSkill);
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGamePlayHumanSkill;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                var code = entity.gamePlayHumanSkill.SkillCode;
                if (code > 0)
                {
                    _contexts.game.gamePlayer.Ani.Attack(code);
                    _contexts.game.gamePlayer.Behaviour.Attack(code);
                }
                else
                {
                    _contexts.game.gamePlayer.Ani.Attack(code);
                }
            }
        }
    }
}
