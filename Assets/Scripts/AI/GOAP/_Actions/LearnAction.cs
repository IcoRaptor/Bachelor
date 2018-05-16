namespace AI.GOAP
{
    public class LearnAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new LearnAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}