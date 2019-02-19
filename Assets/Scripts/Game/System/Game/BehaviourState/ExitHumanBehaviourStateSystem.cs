using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameExitIdelStateSystem : GameHumanBehaviourStateExitSystemBase
    {
        public GameExitIdelStateSystem(Contexts context) : base(context)
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
}
