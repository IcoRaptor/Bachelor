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

        public bool Skip { get; set; }
        public bool Abort { get; set; }

        public int Relevance { get; protected set; }
        public bool Satisfied { get; protected set; }

        #endregion

        public abstract BaseGoal Copy();

        protected abstract void OnFinished(AStarResult result);

        public virtual void UpdateRelevance(Discontentment disc)
        {
            foreach (var index in RelevanceIndices)
                Relevance += disc[index];
        }

        public virtual void Update()
        {
            if (_plan != null && Module != null)
            {
                _plan.Update(Current);

                if (_plan.Finished)
                    Satisfied = true;
            }
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
    }
}