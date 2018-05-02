using AStar;

namespace AI.GOAP
{
    public abstract class BaseGoal : IGOAPImmutable<BaseGoal>
    {
        #region Variables

        protected Plan _plan;

        #endregion

        #region Properties

        public WorldState Target { get; set; }
        public WorldState Current { get; set; }

        public string ID { get; set; }
        public AIModule Module { get; set; }
        public BaseAction[] Actions { get; set; }
        public int[] RelevanceIndices { get; set; }

        public int Relevance { get; protected set; }
        public bool Satisfied { get; protected set; }

        #endregion

        public abstract BaseGoal Copy();

        public abstract void UpdateRelevance(Discontentment disc);

        protected abstract void OnFinished(AStarResult result);

        public virtual void Update()
        {
            if (_plan != null && Module != null)
                _plan.Update(Current);
        }

        public virtual void BuildPlan()
        {
            var goal = new AStarGoalPlanning(this);
            var map = new AStarMapPlanning(goal);

            var asp = new AStarParams()
            {
                Goal = goal,
                Map = map,
                Callback = OnFinished
            };

            AStarMachine.Instance.RunAStar(asp);
        }

        public virtual bool Validate(WorldState current)
        {
            if (_plan == null)
                return false;

            return _plan.Validate(current);
        }
    }
}