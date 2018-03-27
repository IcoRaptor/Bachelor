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

        private void Start()
        {
            _machine = new AStarMachine();
        }

        private void Update()
        {
            _machine.Update();
        }

        private void OnFinished()
        {
            Debug.Log("OnFinished\n");
        }
    }
}