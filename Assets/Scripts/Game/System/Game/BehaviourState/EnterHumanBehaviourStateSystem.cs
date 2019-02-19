using System.Collections.Generic;

namespace Game
{
    //进入Idle状态
    public class GameEnterIdelStateSystem : GameHumanBehaviourStateEnterSystemBase
    {
        public GameEnterIdelStateSystem(Contexts context) : base(context)
        {
        }

        protected override bool StateConditon(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.Behaviour == PlayerBehaviourIndex.IDLE;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.Idle();
        }
    }
    //进入Walk状态
    public class GameEnterWalkStateSystem : GameHumanBehaviourStateEnterSystemBase
    {
        public GameEnterWalkStateSystem(Contexts context) : base(context)
        {
        }

        protected override bool StateConditon(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.Behaviour == PlayerBehaviourIndex.WALK;
        }

        protected override void Execute(List<GameEntity> entities)
        {

        }
    }

    //进入Attack状态
    public class GameEnterAttackStateSystem : GameHumanBehaviourStateEnterSystemBase
    {
        public GameEnterAttackStateSystem(Contexts context) : base(context)
        {
        }

        protected override bool StateConditon(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.Behaviour == PlayerBehaviourIndex.ATTACK;
        }

        protected override void Execute(List<GameEntity> entities)
        {

        }
    }
}
