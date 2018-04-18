namespace AStar
{
    /// <summary>
    /// Represents a graph of AStarNodes
    /// </summary>
    public abstract class AStarMap
    {
        #region Variables

        protected AStarGoal _goal;

        #endregion

        #region Constructors

        public AStarMap(AStarGoal goal)
        {
            _goal = goal;
        }

        #endregion

        public abstract AStarNode CreateNode(AStarNode root, string id);

        public abstract AStarNode[] GetNeighbours(AStarNode node);
    }
}