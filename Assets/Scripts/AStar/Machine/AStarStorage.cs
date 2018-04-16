using AI.GOAP;
using Priority_Queue;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Holds the open and closed lists
    /// </summary>
    public class AStarStorage : IAStarStorage
    {
        #region Variables

        private FastPriorityQueue<AStarNode> _openList;
        private LinkedList<AStarNode> _closedList;

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
            int max = Container.ActionCount() + 10;

            _openList = new FastPriorityQueue<AStarNode>(max);
            _closedList = new LinkedList<AStarNode>();
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

            _closedList.AddLast(node);
            node.OnClosedList = true;

            return true;
        }

        /// <summary>
        /// Returns the node with the lowest f value from the open list
        /// </summary>
        public AStarNode GetBestNode()
        {
            if (!OpenListEmpty)
                return _openList.Dequeue();

            return null;
        }

        /// <summary>
        /// Updates the open list
        /// </summary>
        public void UpdateOpenList(AStarNode node, float f)
        {
            if (node.OnOpenList)
                _openList.UpdatePriority(node, f);
        }

        /// <summary>
        /// Returns all nodes which are part of the solution
        /// </summary>
        public LinkedList<AStarNode> GetFinalPath()
        {
            var list = new LinkedList<AStarNode>();

            foreach (var node in _closedList)
                if (node.PartOfSolution)
                    list.AddLast(node);

            return list;
        }
    }
}