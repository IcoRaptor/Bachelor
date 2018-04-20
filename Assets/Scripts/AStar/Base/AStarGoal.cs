namespace AStar
{
    public abstract class AStarGoal
    {
        public abstract bool IsGoalNode(AStarNode node);

        public abstract float CalcDistanceToTarget(AStarNode node);
    }
}