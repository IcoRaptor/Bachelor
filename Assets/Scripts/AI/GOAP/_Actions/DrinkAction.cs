namespace AI.GOAP
{
    public class DrinkAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new DrinkAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}