using AI.GOAP;

namespace AStar
{
    public class AStarGoalPlanning : AStarGoal
    {
        #region Variables

        private WorldState _target;
        private WorldState _temp;

        private AStarNodePlanning _goal;
        private AStarNodePlanning _start;

        #endregion

        #region Constructors

        public AStarGoalPlanning(WorldState target, WorldState temp, AStarMap map)
            : base(map)
        {
            _target = target;
            _temp = temp;

            _goal = new AStarNodePlanning(_target, -1, -1);
            _start = new AStarNodePlanning(_temp, 0, GetHeuristic(0));
        }

        #endregion

        public override AStarNode GetStartNode()
        {
            return _start;
        }

        public override float GetHeuristic(float g)
        {
            if (_target == null || _temp == null)
                return float.MaxValue;

            float h = _target.GetSymbolDifference(_temp);

            return h * h + g * g;
        }

        public override bool IsGoalNode(AStarNode node)
        {
            var castNode = (AStarNodePlanning)node;
            return _goal.State == castNode.State;
        }
    }
}