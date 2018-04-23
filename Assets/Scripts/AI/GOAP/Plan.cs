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

        private LinkedList<BaseAction> _plan =
            new LinkedList<BaseAction>();

        #endregion

        #region Properties

        public BaseAction CurrentAction { get; private set; }

        #endregion

        #region Constructors

        public Plan(AStarResult result)
        {
            foreach (var node in result.Nodes)
            {
                if (node.Root == null)
                    continue;

                BaseAction action = Container.GetAction(node.ID);
                _plan.AddLast(action);
            }

            // Test
            _plan.AddFirst(Container.GetAction("TestAction"));
        }

        #endregion

        public void Update(AIModule module)
        {
            if (CurrentAction != null)
                CurrentAction.Update(module);
        }

        public void Execute()
        {
            Debug.Log("Executing plan...\n");

            if (_plan.Count == 0)
                return;

            SetupAction();
        }

        public bool Validate()
        {
            if (_plan.Count == 0)
                return false;

            if (CurrentAction.IsActionComplete())
                SetupAction();

            return CurrentAction.IsValid();
        }

        private void SetupAction()
        {
            if (_plan.Count == 0)
                return;

            CurrentAction = _plan.First.Value;
            _plan.RemoveFirst();

            CurrentAction.Activate();
        }
    }
}