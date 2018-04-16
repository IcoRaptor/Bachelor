namespace AI.GOAP
{
    /// <summary>
    /// Provides GOAP functionality for the agent
    /// </summary>
    public sealed class GOAPModule : AIModule
    {
        #region Variables

        private TestGoal _goal;

        #endregion

        private void Start()
        {
            _goal = new TestGoal()
            {
                Module = this
            };

            _goal.UpdateRelevance(new WorldState());
            _goal.BuildPlan();
        }
    }
}