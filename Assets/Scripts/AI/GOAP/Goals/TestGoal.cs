namespace AI.GOAP
{
    public class TestGoal : BaseGoal
    {
        public TestGoal(WorldState target, WorldState current)
        {
            Target = target.Copy();
            Current = current.Copy();
        }

        public override void BuildPlan()
        {
        }

        public override void UpdateRelevance()
        {
        }
    }
}