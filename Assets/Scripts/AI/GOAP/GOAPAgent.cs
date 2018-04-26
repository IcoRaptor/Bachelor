﻿namespace AI.GOAP
{
    public class GOAPAgent
    {
        public string ID { get; set; }
        public BaseGoal[] Goals { get; set; }
        public BaseAction[] Actions { get; set; }

        public GOAPAgent Copy()
        {
            var agent = new GOAPAgent
            {
                ID = ID,
                Goals = new BaseGoal[Goals.Length],
                Actions = new BaseAction[Actions.Length]
            };

            Goals.CopyTo(agent.Goals, 0);
            Actions.CopyTo(agent.Actions, 0);

            for (int i = 0; i < agent.Goals.Length; i++)
                agent.Goals[i] = agent.Goals[i].Copy();

            for (int i = 0; i < agent.Actions.Length; i++)
                agent.Actions[i] = agent.Actions[i].Copy();

            return agent;
        }
    }
}