namespace AStar
{
    public abstract class AStarGoal
    {
        public abstract bool IsGoalNode(AStarNode node);

        public abstract bool Validate();

        public abstract void CalcValues(AStarNode node, out float g, out float h);
    }
}