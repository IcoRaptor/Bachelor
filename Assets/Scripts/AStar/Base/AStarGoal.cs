namespace AStar
{
    public abstract class AStarGoal
    {
        #region Variables

        protected AStarMap _map;

        #endregion

        #region Constructors

        public AStarGoal(AStarMap map)
        {
            _map = map;
        }

        #endregion

        public abstract AStarNode GetStartNode();

        public abstract float GetHeuristic(float g);

        public abstract bool IsGoalNode(AStarNode node);
    }
}