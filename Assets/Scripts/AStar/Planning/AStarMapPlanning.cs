using AI.GOAP;
using System.Collections.Generic;

namespace AStar
{
    public class AStarMapPlanning : AStarMap
    {
        #region Variables

        private static readonly object _lock = new object();

        private static Dictionary<string, AStarNodePlanning> _nodeCache =
           new Dictionary<string, AStarNodePlanning>();

        #endregion

        #region Constructors

        public AStarMapPlanning(AStarGoalPlanning goal) : base(goal)
        {
        }

        #endregion

        /// <summary>
        /// Creates a new node
        ///  (Doesn't set root)
        /// </summary>
        public override AStarNode CreateNode(AStarNode root, string actionID)
        {
            var goalPlanning = (AStarGoalPlanning)_goal;
            var rootPlanning = (AStarNodePlanning)root;

            var current = root == null ?
                goalPlanning.Goal.Current.Copy() :
                rootPlanning.Current;

            var target = root == null ?
                goalPlanning.Goal.Target.Copy() :
                rootPlanning.Target;

            float cost = root == null ?
                0 :
                Container.GetAction(actionID).Cost;

            var node = new AStarNodePlanning(actionID)
            {
                Current = current,
                Target = target,
                Cost = cost
            };

            float g, h;
            _goal.CalculateValues(node, out g, out h);

            node.G = g;
            node.H = h;
            node.Priority = g + h;

            lock (_lock)
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