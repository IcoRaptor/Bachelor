namespace AI.GOAP
{
    public class GoToWorkAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToWorkAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}