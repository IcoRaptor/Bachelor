using UnityEngine;

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
            Debug.Log("Building plan...\n");
            _plan = new Plan();
        }

        public override void UpdateRelevance()
        {
            Debug.Log("Updating relevance to 1...\n");
            _relevance = 1;
        }
    }
}