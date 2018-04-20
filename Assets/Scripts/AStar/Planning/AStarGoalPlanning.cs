using AI.GOAP;

namespace AStar
{
    public class AStarGoalPlanning : AStarGoal
    {
        #region Variables

        private WorldState _target;

        #endregion

        #region Properties

        public BaseGoal Goal { get; private set; }

        #endregion

        #region Constructors

        public AStarGoalPlanning(BaseGoal goal)
        {
            Goal = goal;
            _target = goal.Current; // Search backwards
        }

        #endregion

        public override float CalcDistanceToTarget(AStarNode node)
        {
            var nodePlanning = (AStarNodePlanning)node;
            float distance = nodePlanning.Current.GetSymbolDifference(_target);

            return distance;
        }

        public override bool IsGoalNode(AStarNode node)
        {
            var nodePlanning = (AStarNodePlanning)node;

            return nodePlanning.Current == _target;
        }
    }
}