using Entitas;
using System.Collections.Generic;
using UnityEngine;

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

                //code为0时，代表状态重置
                //code为大于0时，才是正确的执行编码
                _contexts.game.gamePlayer.Ani.Attack(code);
               
                if (code > 0)
                {
                    _contexts.game.gamePlayer.Audio.Attack(code);
                    _contexts.game.gamePlayer.Behaviour.Attack(code);
                }
            }
        }
    }
}
