
namespace BlueGOAP
{
    public interface IGoalManager<TGoal>
    {
        IGoal<TGoal> CurrentGoal { get; }
        void AddGoal(TGoal goalLabel);
        void RemoveGoal(TGoal goalLabel);
        IGoal<TGoal> GetGoal(TGoal goalLabel);
        IGoal<TGoal> FindGoal();
        void UpdateData();
    }
}
