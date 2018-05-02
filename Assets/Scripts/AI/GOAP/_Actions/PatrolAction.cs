namespace AI.GOAP
{
    public class PatrolAction : GoToAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new PatrolAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}