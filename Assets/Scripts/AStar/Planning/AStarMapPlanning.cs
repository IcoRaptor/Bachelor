using AI.GOAP;
using System.Collections.Generic;

namespace AStar
{
    public class AStarMapPlanning : AStarMap
    {
        #region Variables

        private Dictionary<string, AStarNodePlanning> _nodeCache =
           new Dictionary<string, AStarNodePlanning>();

        #endregion

        #region Constructors

        public AStarMapPlanning(AStarGoalPlanning goal) : base(goal)
        {
        }

        #endregion

        /// <summary>
        /// Creates the root node for an A* search
        /// </summary>
        public override AStarNode CreateRootNode()
        {
            var goalPlanning = (AStarGoalPlanning)_goal;

            var root = new AStarNodePlanning(Strings.ROOT_NODE)
            {
                G = 0,
                H = 0,
                Priority = 0,
                Cost = 0,
                Current = goalPlanning.Goal.Target.Copy()   // Search backwards
            };

            _nodeCache[root.ID] = root;

            return root;
        }

        /// <summary>
        /// Creates a new node
        ///  (Doesn't set G and F)
        /// </summary>
        public override AStarNode CreateNode(AStarNode root, string actionID)
        {
            if (_nodeCache.ContainsKey(actionID))
                return _nodeCache[actionID];

            var rootPlanning = (AStarNodePlanning)root;

            var node = new AStarNodePlanning(actionID)
            {
                Root = rootPlanning,
                Current = rootPlanning.Current,
                Cost = Container.GetAction(actionID).Cost
            };

            node.H = _goal.CalcDistanceToTarget(node);

            _nodeCache[node.ID] = node;

            return node;
        }

        /// <summary>
        /// Creates and returns all neighbours
        /// </summary>
        public override AStarNode[] GetNeighbours(AStarNode node)
        {
            // Test
            return new AStarNode[0];
        }
    }
}