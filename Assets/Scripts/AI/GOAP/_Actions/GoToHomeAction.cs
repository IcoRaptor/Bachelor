namespace AI.GOAP
{
    public class GoToHomeAction : GoToAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToHomeAction();
            Setup(action);

            return action;
        }
    }
}