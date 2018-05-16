using UnityEngine;

namespace AI.GOAP
{
    /// <summary>
    /// Provides GOAP functionality for the agent
    /// </summary>
    public sealed class GOAPModule : AIModule
    {
        #region Variables

        [SerializeField]
        private string _agentID = string.Empty;

        private GOAPAgent _agent;
        private Discontentment _disc;

        #endregion

        #region Properties

        public BaseGoal ActiveGoal { get; private set; }

        public WorldState Current { get; private set; }

        #endregion

        private void Start()
        {
            _agent = GOAPContainer.GetAgent(_agentID);

            _disc = new Discontentment();
            Current = new WorldState();

            for (int i = 0; i < WorldState.SymbolCount; i++)
                Current[i] = STATE_SYMBOL.UNSATISFIED;

            foreach (var goal in _agent.Goals)
            {
                goal.Module = this;
                goal.Actions = _agent.Actions;
            }

            UpdateGoals();
            ActiveGoal.BuildPlan();
        }

        private void Update()
        {
            ActiveGoal.Update();

            if (ActiveGoal.Satisfied || ActiveGoal.Abort)
            {
                ActiveGoal.Skip = true;

                if (ActiveGoal.Satisfied)
                    foreach (var index in ActiveGoal.RelevanceIndices)
                        _disc.ClearValue(index);

                UpdateGoals();
                ActiveGoal.BuildPlan();
            }
        }

        public override void Abort()
        {
            ActiveGoal.Abort = true;
        }

        private void UpdateGoals()
        {
            foreach (var goal in _agent.Goals)
            {
                goal.Current = Current;
                goal.UpdateRelevance(_disc);
                goal.Abort = false;
            }

            int highest = int.MinValue;
            int index = 0;

            for (int i = 0; i < _agent.Goals.Length; i++)
            {
                if (_agent.Goals[i].Skip)
                {
                    _agent.Goals[i].Skip = false;
                    continue;
                }

                int relevance = _agent.Goals[i].Relevance;

                if (highest < relevance)
                {
                    highest = relevance;
                    index = i;
                }
            }

            ActiveGoal = _agent.Goals[index];
        }
    }
}