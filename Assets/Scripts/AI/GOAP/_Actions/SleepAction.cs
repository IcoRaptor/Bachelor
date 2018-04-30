namespace AI.GOAP
{
    public class SleepAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new SleepAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}