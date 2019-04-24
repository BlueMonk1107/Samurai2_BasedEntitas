using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueGOAP
{
    public class Planner<TAction, TGoal> : IPlanner<TAction, TGoal>
    {
        private IAgent<TAction, TGoal> _agent;

        public Planner(IAgent<TAction, TGoal> agent)
        {
            _agent = agent;
        }

        public Queue<IActionHandler<TAction>> BuildPlan(IGoal<TGoal> goal)
        {
            DebugMsg.Log("制定计划");
            DebugMsg.Log("---------------当前代理状态------------");
            DebugMsg.Log(_agent.AgentState.ToString());
            DebugMsg.Log("---------------------------");

            Queue<IActionHandler<TAction>> plan = new Queue<IActionHandler<TAction>>();

            if (goal == null)
                return plan;

            TreeNode<TAction> currentNode = Plan(goal);

            if (currentNode == null)
            {
                plan.Enqueue(_agent.ActionManager.GetHandler(_agent.ActionManager.GetDefaultActionLabel()));
                DebugMsg.LogError("当前节点为空，设置当前动作为默认动作");
                return plan;
            }

            while (currentNode.ID != TreeNode<TAction>.DEFAULT_ID)
            {
                plan.Enqueue(currentNode.ActionHandler);
                currentNode = currentNode.ParentNode;
            }

            DebugMsg.Log("---------------最终生成计划------------");
            foreach (IActionHandler<TAction> handler in plan)
            {
                DebugMsg.Log("计划项：" + handler.Label);
            }
            DebugMsg.Log("---------------当前代理状态------------");
            DebugMsg.Log(_agent.AgentState.ToString());
            DebugMsg.Log("---------------------------");
            DebugMsg.Log("计划结束");
            return plan;
        }

        public TreeNode<TAction> Plan(IGoal<TGoal> goal)
        {
            Tree<TAction> tree = new Tree<TAction>();
            //初始化树的头节点
            TreeNode<TAction> topNode = CreateTopNode(tree, goal);

            TreeNode<TAction> cheapestNode = null;
            TreeNode<TAction> currentNode = topNode;
            TreeNode<TAction> subNode = null;
            while (!IsEnd(currentNode))
            {
                //获取所有的子行为
                List<IActionHandler<TAction>> handlers = GetSubHandlers(currentNode);

                DebugMsg.Log("---------------currentNode:"+ currentNode.ID+ "-----------------");
                foreach (IActionHandler<TAction> handler in handlers)
                {
                    DebugMsg.Log("计划子行为:" + handler.Label);
                }
                DebugMsg.Log("--------------------------------");

                foreach (IActionHandler<TAction> handler in handlers)
                {
                    subNode = tree.CreateNode(handler);
                    SetNodeState(currentNode, subNode);
                    subNode.Cost = GetCost(subNode);
                    subNode.ParentNode = currentNode;
                    cheapestNode = GetCheapestNode(subNode, cheapestNode);
                }

                currentNode = cheapestNode;
                cheapestNode = null;
            }


            return currentNode;
        }

        private TreeNode<TAction> CreateTopNode(Tree<TAction> tree, IGoal<TGoal> goal)
        {
            TreeNode<TAction> topNode = tree.CreateTopNode();
            topNode.GoalState.Set(goal.GetEffects());
            topNode.Cost = GetCost(topNode);
            SetNodeCurrentState(topNode);
            return topNode;
        }

        private TreeNode<TAction> GetCheapestNode(TreeNode<TAction> nodeA, TreeNode<TAction> nodeB)
        {
            if (nodeA == null || nodeA.ActionHandler == null)
                return nodeB;

            if (nodeB == null || nodeB.ActionHandler == null)
                return nodeA;

            if (nodeA.Cost > nodeB.Cost)
            {
                return nodeB;
            }
            else if (nodeA.Cost < nodeB.Cost)
            {
                return nodeA;
            }
            else
            {
                if (nodeA.ActionHandler.Action.Priority > nodeB.ActionHandler.Action.Priority)
                {
                    return nodeA;
                }
                else
                {
                    return nodeB;
                }
            }
        }

        private bool IsEnd(TreeNode<TAction> currentNode)
        {
            if (currentNode == null)
                return true;

            if (GetStateDifferecnceNum(currentNode) == 0)
                return true;

            return false;
        }

        private void SetNodeState(TreeNode<TAction> currentNode, TreeNode<TAction> subNode)
        {
            if (subNode.ID > TreeNode<TAction>.DEFAULT_ID)
            {
                IAction<TAction> subAction = subNode.ActionHandler.Action;
                //首先复制当前节点的状态
                subNode.CopyState(currentNode);
                //查找action的effects，和goal中也存在
                IState data = subNode.GoalState.GetSameData(subAction.Effects);
                //那么就把这个状态添加到节点的当前状态中
                subNode.CurrentState.Set(data);
                //把action的先决条件存在goalState中不存在的键值添加进去
                foreach (var key in subAction.Preconditions.GetKeys())
                {
                    if (!subNode.GoalState.ContainKey(key))
                    {
                        subNode.GoalState.Set(key, subAction.Preconditions.Get(key));
                    }
                }
                SetNodeCurrentState(subNode);
            }
        }

        private void SetNodeCurrentState(TreeNode<TAction> node)
        {
            //把GoalState中有且CurrentState没有的添加到CurrentState中
            //数据从agent的当前状态中获取
            var keys = node.CurrentState.GetNotExistKeys(node.GoalState);
            foreach (string key in keys)
            {
                node.CurrentState.Set(key, _agent.AgentState.Get(key));
            }
        }

        private int GetCost(TreeNode<TAction> node)
        {
            int actionCost = 0;
            if (node.ActionHandler != null)
                actionCost = node.ActionHandler.Action.Cost;

            return node.Cost + GetStateDifferecnceNum(node) + actionCost;
        }

        private int GetStateDifferecnceNum(TreeNode<TAction> node)
        {
            return node.CurrentState.GetValueDifferences(node.GoalState).Count;
        }

        /// <summary>
        /// 获取所有的子节点行为
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<IActionHandler<TAction>> GetSubHandlers(TreeNode<TAction> node)
        {
            List<IActionHandler<TAction>> handlers = new List<IActionHandler<TAction>>();

            if (node == null)
                return handlers;

            //获取状态差异
            var keys = node.CurrentState.GetValueDifferences(node.GoalState);
            var map = _agent.ActionManager.EffectsAndActionMap;

            foreach (string key in keys)
            {
                if (map.ContainsKey(key))
                {
                    foreach (IActionHandler<TAction> handler in map[key])
                    {
                        //筛选能够执行的动作
                        if (!handlers.Contains(handler) && handler.Action.Effects.Get(key) == node.GoalState.Get(key))
                        {
                            handlers.Add(handler);
                        }
                    }
                }
                else
                {
                    DebugMsg.LogError("当前没有动作能够实现从当前状态切换到目标状态，无法实现的键值为：" + key);
                }
            }
            //进行优先级排序
            handlers = handlers.OrderByDescending(u => u.Action.Priority).ToList();
            return handlers;
        }
    }
}
