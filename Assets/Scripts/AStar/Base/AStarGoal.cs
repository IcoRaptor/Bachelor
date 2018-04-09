namespace AStar
{
    public abstract class AStarGoal
    {
        public abstract bool IsGoalNode(AStarNode node);

        public abstract void CalculateValues(AStarNode node, out float g, out float h);
    }
}