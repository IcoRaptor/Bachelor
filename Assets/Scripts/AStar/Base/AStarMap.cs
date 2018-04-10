namespace AStar
{
    /// <summary>
    /// Represents a graph of AStarNodes
    /// </summary>
    public abstract class AStarMap
    {
        public abstract AStarNode CreateNewNode(AStarGoal goal, string id);

        public abstract AStarNode[] GetNeighbours(AStarNode node);
    }
}