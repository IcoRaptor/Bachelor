using AStar;

namespace AI.GOAP
{
    public abstract class BaseGoal
    {
        #region Variables

        protected int _relevance;
        protected bool _satisfied;
        protected Plan _plan;

        #endregion

        #region Properties

        public WorldState Target { get; protected set; }
        public WorldState Current { get; protected set; }

        #endregion

        public virtual void BuildPlan()
        {
            var param = new AStarParams()
            {
                Goal = new AStarGoalPlanning(this),
                Map = new AStarMapPlanning(),
                Callback = OnFinished
            };

            AStarMachine.Instance.RunAStar(param);
        }

        public virtual bool Validate()
        {
            if (_plan == null)
                return true;

            return _plan.Validate();
        }

        public abstract void UpdateRelevance();

        protected abstract void OnFinished(AStarResult result);
    }
}