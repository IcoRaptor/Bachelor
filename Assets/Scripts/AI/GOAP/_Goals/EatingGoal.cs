using AStar;
using UnityEngine;

namespace AI.GOAP
{
    public class EatingGoal : BaseGoal
    {
        public override BaseGoal Copy()
        {
            return new EatingGoal()
            {
                ID = ID,
                Actions = Actions,
                Target = Target.Copy()
            };
        }

        public override void UpdateRelevance(Discontentment disc)
        {
            Relevance = 3;
        }

        protected override void OnFinished(AStarResult result)
        {
            if (!Module)
                return;

            Debug.LogFormat("OnFinished\n{0}", result.Code);

            if (result.Code != RETURN_CODE.SUCCESS)
                return;

            _plan = new Plan(result, Module);

            if (_plan.Validate(Current))
                _plan.Execute();
        }
    }
}