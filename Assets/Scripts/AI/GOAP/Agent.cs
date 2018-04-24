namespace AI.GOAP
{
    public class Agent
    {
        public string ID { get; set; }
        public BaseGoal[] Goals { get; set; }
        public BaseAction[] Actions { get; set; }

        public Agent Copy()
        {
            var agent = new Agent
            {
                ID = ID,
                Goals = new BaseGoal[Goals.Length],
                Actions = new BaseAction[Actions.Length]
            };

            Goals.CopyTo(agent.Goals, 0);
            Actions.CopyTo(agent.Actions, 0);

            return agent;
        }
    }
}