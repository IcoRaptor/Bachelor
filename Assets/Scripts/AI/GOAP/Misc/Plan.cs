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
        private AIModule _module;

        #endregion

        #region Properties

        public BaseAction CurrentAction { get; private set; }

        #endregion

        #region Constructors

        public Plan(AStarResult result, AIModule module)
        {
            _plan = new LinkedList<BaseAction>();
            _module = module;

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

        public void Update(WorldState current)
        {
            if (CurrentAction == null)
                return;

            CurrentAction.Update(_module);

            if (!CurrentAction.IsComplete())
                return;

            SetupAction();

            if (!CurrentAction.Validate(current))
                SetupAction();

            if (!CurrentAction.CheckEffects(current))
                SetupAction();
        }

        public void Execute()
        {
            if (_plan.Count == 0)
                return;

            CurrentAction.Activate(_module);
        }

        public bool Validate(WorldState current)
        {
            if (_plan.Count == 0)
                return false;

            if (CurrentAction == null)
                SetupAction();

            if (!CurrentAction.CheckEffects(current))
                SetupAction();

            return CurrentAction.Validate(current);
        }

        private void SetupAction()
        {
            if (_plan.Count == 0)
                return;

            if (CurrentAction != null)
                CurrentAction.Deactivate(_module);

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