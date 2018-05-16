using AStar;

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
                Target = Target.Copy(),
                RelevanceIndices = RelevanceIndices
            };
        }

        protected override void OnFinished(AStarResult result)
        {
            if (!Module)
                return;

            if (result.Code != RETURN_CODE.SUCCESS)
            {
                Abort = true;
                return;
            }

            _plan = new Plan(result, Module);
            _plan.Execute();
        }
    }
}