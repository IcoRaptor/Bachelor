using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Holds the open and closed lists
    /// </summary>
    public class AStarStorage
    {
        #region Variables

#pragma warning disable 0414
        private static Dictionary<string, object> _actionCache =
            new Dictionary<string, object>();

        private List<AStarNode> _openList = new List<AStarNode>();
        private List<AStarNode> _closedList = new List<AStarNode>();

        private int _openIndex = 0;
#pragma warning restore

        #endregion

        #region Properties

        public bool IsOpenListEmpty
        {
            get { return _openList.Count == 0; }
        }

        #endregion

        /// <summary>
        /// Adds an action to the action cache
        /// </summary>
        public static bool AddAction(object action)
        {
            if (_actionCache.ContainsKey(action.ToString()))
                return false;

            _actionCache.Add(action.ToString(), action);

            return true;
        }

        /// <summary>
        /// Adds a node to the open list
        /// </summary>
        public bool AddNodeToOpenList(AStarNode node)
        {
            if (node.OnOpenList || node.OnClosedList)
                return false;

            _openList.Add(node);

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
            _closedList.Add(node);

            node.OnClosedList = true;

            return true;
        }

        public AStarNode GetNextNode()
        {
            return _openList[_openIndex++];
        }
    }
}