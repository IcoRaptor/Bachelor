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

            for (int i = 0; i < Target.Symbols.Length; i++)
                Target.Symbols[i] = STATE_SYMBOL.SATISFIED;
        }

        public override void BuildPlan()
        {
            Debug.Log("TestGoal: Building plan...\n");
            base.BuildPlan();
        }

        public override void UpdateRelevance(Discontentment disc)
        {
            for (int i = 0; i < Current.Symbols.Length; i++)
                Current.Symbols[i] = STATE_SYMBOL.SATISFIED;

            Relevance = 1;
        }

        protected override void OnFinished(AStarResult result)
        {
            if (!Module)
                return;

            Debug.LogFormat("OnFinished\n{0}", result.Code);

            if (result.Code != RETURN_CODE.SUCCESS)
                return;

            _plan = new Plan(result);
            _plan.Execute();
        }

        public override BaseGoal Copy()
        {
            var goal = new TestGoal()
            {
                ID = ID,
                Current = Current.Copy(),
                Target = Target.Copy(),
            };

            return goal;
        }
    }
}