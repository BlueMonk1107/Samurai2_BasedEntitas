using System;
using System.Collections.Generic;
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
            AddAction<InjureUpHandler, InjureUpAction>();
            AddAction<InjureDownHandler, InjureDownAction>();
            AddAction<InjureLeftHandler, InjureLeftAction>();
            AddAction<InjureRightHandler, InjureRightAction>();
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

        protected override void InitGameData(Action<IAgent<ActionEnum, GoalEnum>, IMaps<ActionEnum, GoalEnum>> onInitGameData)
        {
            base.InitGameData(onInitGameData);
            SetGameData(GameDataKeyEnum.INJURE_COLLODE_DATA,new Dictionary<ActionEnum,bool>());
        }
    }
}
