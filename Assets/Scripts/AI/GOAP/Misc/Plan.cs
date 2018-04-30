using AStar;
using System.Collections.Generic;
using UnityEngine;

namespace AI.GOAP
{
    /// <summary>
    /// Represents a list of valid actions
    /// </summary>
    public class Plan
    {
        #region Variables

        private LinkedList<BaseAction> _plan;

        #endregion

        #region Properties

        public BaseAction CurrentAction { get; private set; }

        #endregion

        #region Constructors

        public Plan(AStarResult result)
        {
            _plan = new LinkedList<BaseAction>();

            foreach (var node in result.Nodes)
            {
                if (node.Root == null)
                    continue;

                BaseAction action = GOAPContainer.GetAction(node.ID);
                _plan.AddFirst(action);
            }

            Debug.Log(ToString() + "\n");
        }

        #endregion

        public void Update(AIModule module)
        {
            if (CurrentAction != null)
                CurrentAction.Update(module);
        }

        public void Execute()
        {
            if (_plan.Count == 0)
                return;

            CurrentAction.Activate();
        }

        public bool Validate(WorldState current)
        {
            if (_plan.Count == 0)
                return false;

            if (CurrentAction == null || CurrentAction.IsComplete())
                SetupAction();

            return CurrentAction.Validate(current);
        }

        private void SetupAction()
        {
            if (_plan.Count == 0)
                return;

            CurrentAction = _plan.First.Value;
            _plan.RemoveFirst();
        }

        public override string ToString()
        {
            string s = "Plan:\n";

            foreach (var action in _plan)
                s += action.ID + " ";

            return s;
        }
    }
}