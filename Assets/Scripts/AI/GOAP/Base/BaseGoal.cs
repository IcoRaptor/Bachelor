using AStar;

namespace AI.GOAP
{
    public abstract class BaseGoal
    {
        #region Variables

        protected Plan _plan;

        #endregion

        #region Properties

        public WorldState Target { get; protected set; }
        public WorldState Current { get; protected set; }

        public GOAPModule Module { get; set; }

        public string ID { get; set; }
        public int Relevance { get; protected set; }
        public bool Satisfied { get; protected set; }

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
                return false;

            return _plan.Validate();
        }

        public abstract void UpdateRelevance(WorldState current);

        protected abstract void OnFinished(AStarResult result);
    }
}