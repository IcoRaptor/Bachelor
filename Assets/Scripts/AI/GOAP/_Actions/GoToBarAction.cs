namespace AI.GOAP
{
    public class GoToBarAction : GoToAction
    {
        public override void Activate(AIModule module)
        {
            _target = PositionCollection.Instance.Bar;
            base.Activate(module);
        }

        public override BaseAction Copy()
        {
            var action = new GoToBarAction();
            Setup(action);

            return action;
        }

        public override bool CheckContext()
        {
            return true;
        }
    }
}