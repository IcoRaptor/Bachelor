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
            get { return _openList.Count <= 0; }
        }

        #endregion

        #region Constructors

        public AStarStorage()
        {
            int max = GOAPContainer.ActionCount + 10;

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
            if (node.OnClosedList)
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
            AStarNode node = null;

            if (!OpenListEmpty)
                node = _openList.Dequeue();

            return node;
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

            // Check last entry first
            var last = _closedList.Last.Value;
            _closedList.RemoveLast();

            if (last.SolutionNode)
                HandleSolutionNode(last, list);
            else
            {
                foreach (var node in _closedList)
                {
                    if (!node.SolutionNode)
                        continue;

                    HandleSolutionNode(node, list);
                }
            }

            _closedList.Clear();
            _openList.Clear();

            return list;
        }

        private void HandleSolutionNode(AStarNode node, LinkedList<AStarNode> list)
        {
            list.AddFirst(node);

            var root = node.Root;

            while (root != null)
            {
                list.AddFirst(root);
                root = root.Root;
            }
        }
    }
}