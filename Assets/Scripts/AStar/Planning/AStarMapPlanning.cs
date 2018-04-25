using AI.GOAP;
using System.Collections.Generic;

namespace AStar
{
    public class AStarMapPlanning : AStarMap
    {
        #region Variables

        private Dictionary<string, AStarNodePlanning> _nodeCache;

        #endregion

        #region Constructors

        public AStarMapPlanning(AStarGoalPlanning goal) : base(goal)
        {
            _nodeCache = new Dictionary<string, AStarNodePlanning>();
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
        public override AStarNode CreateNode(AStarNode root, string id)
        {
            var rootPlanning = (AStarNodePlanning)root;
            AStarNodePlanning node = null;

            if (_nodeCache.ContainsKey(id))
                node = _nodeCache[id];
            else
                node = new AStarNodePlanning(id);

            node.Root = rootPlanning;
            node.Current = rootPlanning.Current;
            node.Cost = GOAPContainer.GetAction(id).Cost;
            node.H = _goal.CalcDistanceToTarget(node);

            _nodeCache[id] = node;

            return node;
        }

        /// <summary>
        /// Creates and returns the neighbouring nodes
        /// </summary>
        public override AStarNode[] GetNeighbours(AStarNode node)
        {
            var goalPlanning = (AStarGoalPlanning)_goal;
            var nodePlanning = (AStarNodePlanning)node;

            var actions = GOAPContainer.ChooseActions(
                nodePlanning.Current,
                goalPlanning.Goal.Actions);

            var nodes = new AStarNode[actions.Length];

            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = CreateNode(nodePlanning, actions[i].ID);

            return nodes;
        }
    }
}