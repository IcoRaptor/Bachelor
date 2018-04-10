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
            var test = new WorldState();
            var testGoal = new TestGoal(test, test);

            _map = new AStarMapPlanning();
            _goal = new AStarGoalPlanning(testGoal);

            var aSP = new AStarParams()
            {
                Goal = _goal,
                Map = _map,
                Callback = OnFinished
            };

            _machine = new AStarMachine();
            _machine.RunAStar(aSP);
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