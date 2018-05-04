namespace AI.GOAP
{
    public class GoToStoreAction : GoToAction
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
    }
}