using AStar;
using System.Collections.Generic;

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
                _plan.AddLast(action);
            }
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

            SetupAction();
        }

        public bool Validate()
        {
            if (_plan.Count == 0)
                return false;

            if (CurrentAction.IsComplete())
                SetupAction();

            return CurrentAction.Validate();
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