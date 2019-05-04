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
            AddAction<InjureHandler, InjureUpAction>();
            AddAction<InjureHandler, InjureDownAction>();
            AddAction<InjureHandler, InjureLeftAction>();
            AddAction<InjureHandler, InjureRightAction>();
            AddAction<MoveBackwardHandler, MoveBackwardAction>();

            AddAction<DeadNormalHandler, DeadNormalAction>();
            AddAction<DeadIntantKillHandler,DeadHeadAction>();
            AddAction<DeadIntantKillHandler, DeadBodyAction>();
            AddAction<DeadIntantKillHandler, DeadLegAction>();

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
