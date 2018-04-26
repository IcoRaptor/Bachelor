namespace AStar
{
    public abstract class AStarGoal
    {
        public abstract bool CheckNode(AStarNode node);

        public abstract float DistanceToTarget(AStarNode node);
    }
}