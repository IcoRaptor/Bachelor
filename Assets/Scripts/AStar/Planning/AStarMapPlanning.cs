using AI.GOAP;

namespace AStar
{
    public class AStarMapPlanning : AStarMap
    {
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

            return root;
        }

        /// <summary>
        /// Creates a new node
        ///  (Doesn't set G and F)
        /// </summary>
        public override AStarNode CreateNode(AStarNode root, string actionID)
        {
            var rootPlanning = (AStarNodePlanning)root;

            var node = new AStarNodePlanning(actionID)
            {
                Root = rootPlanning,
                Current = rootPlanning.Current,
                Cost = GOAPContainer.GetAction(actionID).Cost
            };

            node.H = _goal.CalcDistanceToTarget(node);

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