namespace AI.GOAP
{
    public abstract class BaseGoal
    {
        #region Variables

        protected bool _satisfied;
        protected WorldState _target;
        protected Discontentment _discontentment;

        #endregion

        public abstract void UpdateRelevance();

        public abstract void BuildPlan();
    }
}