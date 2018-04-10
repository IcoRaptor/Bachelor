namespace AStar
{
    public class AStarMapPlanning : AStarMap
    {
        /// <summary>
        /// Creates a new node.
        /// </summary>
        public override AStarNode CreateNewNode(AStarGoal goal, string actionID)
        {
            var goalPlanning = (AStarGoalPlanning)goal;

            var node = new AStarNodePlanning(actionID)
            {
                Current = goalPlanning.Goal.Current.Copy(),
                Target = goalPlanning.Goal.Target.Copy()
            };

            float g, h;
            goal.CalculateValues(node, out g, out h);

            node.G = g;
            node.H = h;
            node.Priority = g + h;

            return node;
        }

        /// <summary>
        /// Creates and returns all neighbours
        /// </summary>
        public override AStarNode[] GetNeighbours(AStarNode node)
        {
            return new AStarNode[0];
        }
    }
}