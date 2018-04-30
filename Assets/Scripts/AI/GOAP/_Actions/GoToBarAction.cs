﻿namespace AI.GOAP
{
    public class GoToBarAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToBarAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}