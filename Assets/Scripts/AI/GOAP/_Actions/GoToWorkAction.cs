namespace AI.GOAP
{
    public class GoToWorkAction : GoToAction
    {
        public override void Activate(AIModule module)
        {
            _target = PositionCollection.Instance.Work;
            base.Activate(module);
        }

        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToWorkAction();
            Setup(action);

            return action;
        }
    }
}