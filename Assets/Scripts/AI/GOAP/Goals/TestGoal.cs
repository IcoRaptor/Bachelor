using AStar;
using UnityEngine;

namespace AI.GOAP
{
    public class TestGoal : BaseGoal
    {
        public TestGoal()
        {
            Target = new WorldState();
            Current = new WorldState();
        }

        public override void BuildPlan()
        {
            Debug.Log("TestGoal: Building plan...\n");

            base.BuildPlan();
        }

        public override void UpdateRelevance()
        {
            Debug.Log("Updating relevance to 1...\n");
            _relevance = 1;
        }

        protected override void OnFinished(AStarResult result)
        {
            Debug.LogFormat("OnFinished\n{0}", result.Code);

            _plan = new Plan(result);
            _plan.Execute();
        }
    }
}