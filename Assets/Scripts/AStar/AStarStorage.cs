using Priority_Queue;

namespace AStar
{
    /// <summary>
    /// Holds the open and closed lists
    /// </summary>
    public class AStarStorage
    {
        #region Variables

        private FastPriorityQueue<AStarNode> _openList;
        private FastPriorityQueue<AStarNode> _closedList;

        #endregion

        #region Properties

        public bool OpenListEmpty
        {
            get { return _openList.Count == 0; }
        }

        #endregion

        #region Constructors

        public AStarStorage()
        {
            _openList = new FastPriorityQueue<AStarNode>(10);
            _closedList = new FastPriorityQueue<AStarNode>(10);
        }

        #endregion

        /// <summary>
        /// Adds a node to the open list
        /// </summary>
        public bool AddNodeToOpenList(AStarNode node)
        {
            if (node.OnOpenList || node.OnClosedList)
                return false;

            _openList.Enqueue(node, node.Priority);
            node.OnOpenList = true;

            return true;
        }

        /// <summary>
        /// Adds a node to the closed list
        /// </summary>
        public bool AddNodeToClosedList(AStarNode node)
        {
            if (node.OnClosedList || !node.OnOpenList)
                return false;

            _openList.Remove(node);
            _closedList.Enqueue(node, node.Priority);
            node.OnClosedList = true;

            return true;
        }

        /// <summary>
        /// Returns the node with the lowest f value from the open list
        /// </summary>
        public AStarNode GetNextBestNode()
        {
            if (!OpenListEmpty)
                return _openList.First;

            return null;
        }

        /// <summary>
        /// Updates the open and closed lists
        /// </summary>
        public void UpdateLists(AStarNode node, float f)
        {
            if (!node.OnOpenList && !node.OnClosedList)
                return;

            if (node.OnOpenList)
                _openList.UpdatePriority(node, f);
            else
                _closedList.UpdatePriority(node, f);
        }
    }
}