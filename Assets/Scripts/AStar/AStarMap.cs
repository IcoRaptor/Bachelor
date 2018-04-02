namespace AStar
{
    /// <summary>
    /// Represents a graph of AStarNodes
    /// </summary>
    public class AStarMap
    {
        public AStarNode GetNextNode(AStarStorage storage)
        {
            if (storage.IsOpenListEmpty)
                return null;

            return storage.GetNextNode();
        }
    }
}