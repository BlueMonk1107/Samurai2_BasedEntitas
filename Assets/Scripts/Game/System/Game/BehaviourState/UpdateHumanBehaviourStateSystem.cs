using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameUpdateIdelStateSystem : GameHumanBehaviourStateUpdateSystemBase
    {
        public GameUpdateIdelStateSystem(Contexts context) : base(context)
        {
        }
        protected override bool StateConditon(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.Behaviour == PlayerBehaviourIndex.IDLE;
        }

        protected override void Execute(List<GameEntity> entities)
        {

        }
    }

    public class GameUpdateMoveStateSystem : GameHumanBehaviourStateUpdateSystemBase
    {
        public GameUpdateMoveStateSystem(Contexts context) : base(context)
        {
        }
        protected override bool StateConditon(GameEntity entity)
        {
            return entity.gameHumanBehaviourState.Behaviour == PlayerBehaviourIndex.WALK
                || entity.gameHumanBehaviourState.Behaviour == PlayerBehaviourIndex.RUN;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _contexts.game.gamePlayer.Behaviour.Move();
            _contexts.game.gamePlayer.Audio.Move();
        }
    }
}
