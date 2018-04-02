﻿using AStar;
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

        #endregion

        private void Start()
        {
            _machine = new AStarMachine();
            _machine.RunAStar(
                new GOAPGoal(),
                new AStarMap(),
                OnFinished);
        }

        private void Update()
        {
            _machine.Update();
        }

        private void OnFinished(RETURN_CODE code)
        {
            Debug.LogFormat("OnFinished\n{0}", code);
        }
    }
}