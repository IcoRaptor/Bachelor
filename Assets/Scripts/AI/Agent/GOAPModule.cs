using AStar;
using UnityEngine;

namespace AI.GOAP
{
    /// <summary>
    /// Provides GOAP functionality for the agent
    /// </summary>
    public sealed class GOAPModule : AIModule
    {
        #region Variables

        private AStarMachine _machine;
        private AStarGoalPlanning _goal;
        private AStarMapPlanning _map;

        #endregion

        private void Start()
        {
            _map = new AStarMapPlanning();
            _goal = new AStarGoalPlanning(new TestGoal(
                    new WorldState(),
                    new WorldState()));

            _machine = new AStarMachine();
            _machine.RunAStar(_goal, _map, OnFinished);
        }

        private void Update()
        {
            _machine.Update();
        }

        private void OnFinished(AStarResult result)
        {
            Debug.LogFormat("OnFinished\n{0}", result.Code);
        }
    }
}