using AStar;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Controls the agent
    /// </summary>
    public sealed class AIAgent : BaseAgent
    {
        #region Variables

        private AStarMachine _machine;

        #endregion

        public void ExecuteAction()
        {
        }

        private void Start()
        {
            _machine = new AStarMachine();
            _machine.RunAStar(new AStarGoal(), new AStarMap());
        }

        private void Update()
        {
            _machine.CheckFinished(() => OnFinished());
        }

        private void OnFinished()
        {
            Debug.Log(transform.position);
            _machine.RunAStar(new AStarGoal(), new AStarMap());
        }
    }
}