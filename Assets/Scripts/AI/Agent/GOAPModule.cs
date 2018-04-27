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

        public WorldState Current { get; set; }

        #endregion

        private void Start()
        {
            _agent = GOAPContainer.GetAgent(_agentID);

            Current = new WorldState();
            _disc = new Discontentment();

            for (int i = 0; i < WorldState.NUM_SYMBOLS; i++)
                Current.Symbols[i] = STATE_SYMBOL.ERROR;

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
        }

        private void UpdateGoals()
        {
            foreach (var goal in _agent.Goals)
            {
                goal.Current = Current;
                goal.UpdateRelevance(_disc);
            }

            int highest = int.MinValue;
            int index = 0;

            for (int i = 0; i < _agent.Goals.Length; i++)
            {
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