namespace AI.GOAP
{
    public abstract class BaseGoal
    {
        #region Variables

        protected bool _satisfied;
        protected Plan _plan;

        #endregion

        #region Properties

        public WorldState Target { get; protected set; }
        public WorldState Current { get; protected set; }

        #endregion

        public abstract void UpdateRelevance();

        public abstract void BuildPlan();

        /// <summary>
        /// Returns the difference between the target and current WS
        /// </summary>
        public int GetDifference()
        {
            return Target.GetSymbolDifference(Current);
        }
    }
}