namespace AI.GOAP
{
    public class GoToStoreAction : GoToAction
    {
        public override void Activate(AIModule module)
        {
            _target = PositionCollection.Instance.Shop;
            base.Activate(module);
        }

        public override BaseAction Copy()
        {
            var action = new GoToStoreAction();
            Setup(action);

            return action;
        }

        public override bool CheckContext()
        {
            return true;
        }
    }
}