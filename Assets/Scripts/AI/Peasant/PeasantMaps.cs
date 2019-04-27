using System;
using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class PeasantMaps : MapsBase<ActionEnum, GoalEnum>
    {
        public PeasantMaps(IAgent<ActionEnum, GoalEnum> agent, 
            Action<IAgent<ActionEnum, GoalEnum>, IMaps<ActionEnum, GoalEnum>> onInitGameData)
            : base(agent, onInitGameData)
        {
        }

        protected override void InitActinMaps()
        {
            AddAction<IdleHandler, IdleAction>();
            AddAction<IdleSwordHandler, IdleSwordAction>();
            AddAction<MoveHandler, MoveAction>();
            AddAction<AttackHandler, AttackAction>();
            AddAction<InjureHandler, InjureAction>();
            AddAction<MoveBackwardHandler, MoveBackwardAction>();
            AddAction<DeadHandler, DeadAction>();
            AddAction<EnterAlertHandler,EnterAlertAction>();
            AddAction<ExitAlertHandler,ExitAlertAction>();

            AddAction<AlertStateHandler,AlertActionState>();
        }

        protected override void InitGoalMaps()
        {
            AddGoal<IdleSwordGoal>();
            AddGoal<AttackGoal>();
            AddGoal<DeadGoal>();
            AddGoal<InjureGoal>();
        }
    }
}
