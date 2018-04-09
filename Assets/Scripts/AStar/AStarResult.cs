using AI.GOAP;

namespace AStar
{
    public struct AStarResult
    {
        public RETURN_CODE Code { get; set; }
        public Plan FinishedPlan { get; set; }

        public AStarResult(RETURN_CODE code, Plan plan)
        {
            Code = code;
            FinishedPlan = plan;
        }
    }
}