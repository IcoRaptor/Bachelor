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

            foreach (var n in result.Nodes)
            {
                BaseAction action = Container.GetAction(n.ID);

                if (action == null)
                    continue;

                _plan.AddFirst(action);
            }
        }

        #endregion

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

            if (CurrentAction.IsActionComplete())
                SetupAction();

            return CurrentAction.IsValid();
        }

        private void SetupAction()
        {
            CurrentAction = _plan.First.Value;
            _plan.RemoveFirst();

            CurrentAction.Activate();
        }
    }
}