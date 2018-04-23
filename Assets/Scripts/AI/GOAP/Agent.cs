namespace AI.GOAP
{
    public class Agent
    {
        public string ID { get; set; }
        public BaseGoal[] Goals { get; set; }
        public BaseAction[] Actions { get; set; }
    }
}