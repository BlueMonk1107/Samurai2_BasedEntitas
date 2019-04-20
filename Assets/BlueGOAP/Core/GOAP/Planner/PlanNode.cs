
namespace BlueGOAP
{
    public class PlanNode<TAction>
    {
        public TAction ActionLabel { get; set; }
        public IState CurrentState { get; set; }
        public IState GoalState { get; set; }
        public PlanNode<TAction> Parent { get; set; }

    }
}
