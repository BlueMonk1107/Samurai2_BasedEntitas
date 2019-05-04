using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public abstract class DeadAction : ActionBase<ActionEnum, GoalEnum>
    {
        public override int Cost { get { return 1; } }
        public override int Priority { get { return _priority; } }
        public override bool CanInterruptiblePlan { get { return true; } }
        protected abstract int DefaultPriority { get; }
        private int _priority;
        protected const int DEFAULT_PRIORITY = 1000;

        public DeadAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
            _priority = DefaultPriority;
        }

        public void ChangePriority(bool isChange)
        {
            if (isChange)
            {
                _priority = DefaultPriority + 2;
            }
            else
            {
                _priority = DefaultPriority;
            }
        }


        protected override IState InitPreconditions()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_DEAD, true);
            return state;
        }

        protected override IState InitEffects()
        {
            State<StateKeyEnum> state = new State<StateKeyEnum>();
            state.Set(StateKeyEnum.IS_OVER, true);
            return state;
        }
    }

    public class DeadNormalAction : DeadAction
    {
        public override ActionEnum Label { get {return ActionEnum.DEAD;} }
        protected override int DefaultPriority { get { return DEFAULT_PRIORITY + 1; } }

        public DeadNormalAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }
    }

    public class DeadHeadAction : DeadAction
    {
        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_HEAD; } }
        protected override int DefaultPriority { get { return DEFAULT_PRIORITY + 2; } }

        public DeadHeadAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }
    }

    public class DeadBodyAction : DeadAction
    {
        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_BODY; } }
        protected override int DefaultPriority { get { return DEFAULT_PRIORITY + 2; } }

        public DeadBodyAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }
    }

    public class DeadLegAction : DeadAction
    {
        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_LEG; } }
        protected override int DefaultPriority { get { return DEFAULT_PRIORITY + 2; } }

        public DeadLegAction(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }
    }
}
