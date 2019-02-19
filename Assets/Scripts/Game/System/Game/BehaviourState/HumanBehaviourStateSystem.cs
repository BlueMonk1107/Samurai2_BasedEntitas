using Entitas;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// 人物行为状态响应函数父类
    /// </summary>
    public abstract class GameHumanBehaviourStateSystemBase : ReactiveSystem<GameEntity>
    {
        protected Contexts _contexts;

        public GameHumanBehaviourStateSystemBase(Contexts context) : base(context.game)
        {
            _contexts = context;
        }
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.GameHumanBehaviourState);
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGameHumanBehaviourState && FilterCondition(entity);
        }

        protected abstract bool FilterCondition(GameEntity entity);
    }

    /// <summary>
    /// 进入人物行为状态响应函数父类
    /// </summary>
    public abstract class GameHumanBehaviourStateEnterSystemBase : GameHumanBehaviourStateSystemBase
    {

        public GameHumanBehaviourStateEnterSystemBase(Contexts context) : base(context)
        {
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGameHumanBehaviourState && FilterCondition(entity);
        }

        protected override bool FilterCondition(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.State == BehaviorState.ENTER;
        }

        protected abstract bool StateConditon(GameEntity entity);
    }
    /// <summary>
    /// 正在执行人物行为状态响应函数父类
    /// </summary>
    public abstract class GameHumanBehaviourStateUpdateSystemBase : GameHumanBehaviourStateSystemBase
    {

        public GameHumanBehaviourStateUpdateSystemBase(Contexts context) : base(context)
        {
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGameHumanBehaviourState && FilterCondition(entity) && StateConditon(entity);
        }

        protected override bool FilterCondition(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.State == BehaviorState.UPDATE;
        }

        protected abstract bool StateConditon(GameEntity entity);
    }
    /// <summary>
    /// 退出人物行为状态响应函数父类
    /// </summary>
    public abstract class GameHumanBehaviourStateExitSystemBase : GameHumanBehaviourStateSystemBase
    {

        public GameHumanBehaviourStateExitSystemBase(Contexts context) : base(context)
        {
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGameHumanBehaviourState && FilterCondition(entity);
        }

        protected override bool FilterCondition(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.State == BehaviorState.EXIT;
        }

        protected abstract bool StateConditon(GameEntity entity);
    }
}
