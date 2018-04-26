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
            var pGoal = (AStarGoalPlanning)_goal;

            var root = new AStarNodePlanning(Strings.ROOT_NODE)
            {
                G = 0,
                H = 0,
                Priority = 0,
                Cost = 0,
                Current = pGoal.Current
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
            var pRoot = (AStarNodePlanning)root;

            var node = _nodeCache.ContainsKey(id) ?
                _nodeCache[id] :
                new AStarNodePlanning(id);

            // Initialize node
            node.Root = pRoot;
            node.Current = pRoot.Current;
            node.Cost = GOAPContainer.GetAction(id).Cost;
            node.H = _goal.DistanceToTarget(node);

            _nodeCache[id] = node;

            return node;
        }

        /// <summary>
        /// Creates and returns the neighbouring nodes
        /// </summary>
        public override AStarNode[] GetNeighbours(AStarNode node)
        {
            var pGoal = (AStarGoalPlanning)_goal;
            var pNode = (AStarNodePlanning)node;

            var actions = GOAPContainer.ChooseActions(
                pNode.Current,
                pGoal.Actions);

            var nodes = new AStarNode[actions.Length];

            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = CreateNode(pNode, actions[i].ID);

            return nodes;
        }
    }
}