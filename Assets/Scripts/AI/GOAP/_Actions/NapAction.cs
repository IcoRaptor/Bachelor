namespace AI.GOAP
{
    public class NapAction : BaseAction
    {
        public override bool CheckContext()
        {
            return false;
        }

        public override BaseAction Copy()
        {
            var action = new NapAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}