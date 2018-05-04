namespace AI.GOAP
{
    public class GoToPlaygroundAction : GoToAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToPlaygroundAction();
            Setup(action);

            return action;
        }
    }
}