using UnityEngine;

namespace AI.GOAP
{
    /// <summary>
    /// Provides GOAP functionality for the agent
    /// </summary>
    public sealed class GOAPModule : AIModule
    {
        #region Variables

        [SerializeField]
        private string _agentID;

        private BaseGoal[] _goals;
        private BaseAction[] _actions;

        #endregion

        #region Properties

        public BaseGoal ActiveGoal { get; private set; }

        #endregion

        private void Start()
        {
            Container.GetAgent(
                _agentID,
                out _goals,
                out _actions);

            foreach (var goal in _goals)
            {
                goal.Module = this;
                goal.AgentID = _agentID;
            }

            // Test
            ActiveGoal = Container.GetGoal("TestGoal");
            ActiveGoal.Module = this;
            ActiveGoal.AgentID = _agentID;

            ActiveGoal.UpdateRelevance(new WorldState());
            ActiveGoal.BuildPlan();
        }

        private void Update()
        {
            ActiveGoal.Update();
        }
    }
}