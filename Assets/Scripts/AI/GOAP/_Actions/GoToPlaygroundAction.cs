namespace AI.GOAP
{
    public class GoToPlaygroundAction : GoToAction
    {
        public override void Activate(AIModule module)
        {
            _target = PositionCollection.Instance.Playground;
            base.Activate(module);
        }

        public override BaseAction Copy()
        {
            var action = new GoToPlaygroundAction();
            Setup(action);

            return action;
        }

        public override bool CheckContext()
        {
            return true;
        }
    }
}