using System;
using BlueGOAP;
using UnityEngine;

namespace Game.UI
{
    public class PeasantMaps : MapsBase<ActionEnum, GoalEnum>
    {
        public PeasantMaps(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
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

            AddAction<AlertHandler,AlertAction>();
        }

        protected override void InitGoalMaps()
        {
            AddGoal<IdleSwordGoal>();
            AddGoal<AttackGoal>();
            AddGoal<DeadGoal>();
            AddGoal<InjureGoal>();
        }

        protected override void InitGameData()
        {
            throw new NotImplementedException();
        }
    }
}
