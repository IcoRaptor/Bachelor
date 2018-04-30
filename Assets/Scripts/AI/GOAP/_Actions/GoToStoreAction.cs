namespace AI.GOAP
{
    public class GoToStoreAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToStoreAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}