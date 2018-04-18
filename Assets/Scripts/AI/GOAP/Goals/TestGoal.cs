using AStar;
using UnityEngine;

namespace AI.GOAP
{
    public class TestGoal : BaseGoal
    {
        public TestGoal()
        {
            Target = new WorldState();

            for (int i = 0; i < Target.Symbols.Length; i++)
                Target.Symbols[i] = STATE_SYMBOL.SATISFIED;
        }

        public override void BuildPlan()
        {
            Debug.Log("TestGoal: Building plan...\n");
            base.BuildPlan();
        }

        public override void UpdateRelevance(WorldState current)
        {
            Current = new WorldState();

            for (int i = 0; i < Current.Symbols.Length; i++)
                Current.Symbols[i] = STATE_SYMBOL.SATISFIED;

            Relevance = 1;
        }

        protected override void OnFinished(AStarResult result)
        {
            if (!Module)
            {
                Debug.Log("No module found!\n");
                return;
            }

            Debug.LogFormat("OnFinished\n{0}", result.Code);

            if (result.Code != RETURN_CODE.SUCCESS)
                return;

            _plan = new Plan(result);
            _plan.Execute();
        }
    }
}