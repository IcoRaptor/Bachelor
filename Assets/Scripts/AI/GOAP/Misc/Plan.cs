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
        private AIModule _module;

        #endregion

        #region Properties

        public BaseAction CurrentAction { get; private set; }
        public bool Finished { get; private set; }

        #endregion

        #region Constructors

        public Plan(AStarResult result, AIModule module)
        {
            _plan = new LinkedList<BaseAction>();
            _module = module;

            _module.Board.Dialog = string.Empty;

            foreach (var node in result.Nodes)
            {
                if (node.Root == null)
                    continue;

                BaseAction action = GOAPContainer.GetAction(node.ID);
                _plan.AddFirst(action);
            }

            var dummy = new WorldState();
            SetupNewAction(ref dummy);
        }

        #endregion

        public void Update(WorldState current)
        {
            if (CurrentAction == null)
                return;

            CurrentAction.Update(_module);

            if (CurrentAction.IsComplete())
            {
                SetupNewAction(ref current);

                if (CurrentAction == null)
                    return;

                if (!CurrentAction.Validate(current))
                {
                    _module.Abort();
                    return;
                }

                if (!CurrentAction.CheckEffects(current))
                    SetupNewAction(ref current);

                CurrentAction.Activate(_module);
            }
        }

        public void Execute()
        {
            CurrentAction.Activate(_module);
        }

        private void SetupNewAction(ref WorldState current)
        {
            if (CurrentAction != null)
            {
                current = CurrentAction.Deactivate(_module, current);

                if (CurrentAction.LastAction)
                {
                    Finished = true;
                    CurrentAction = null;
                    return;
                }
            }

            CurrentAction = _plan.First.Value;
            _plan.RemoveFirst();

            if (_plan.First == null)
                CurrentAction.LastAction = true;
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