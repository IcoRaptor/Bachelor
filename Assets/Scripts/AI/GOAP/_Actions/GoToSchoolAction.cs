namespace AI.GOAP
{
    public class GoToSchoolAction : GoToAction
    {
        public override void Activate(AIModule module)
        {
            _target = PositionCollection.Instance.School;
            base.Activate(module);
        }

        public override BaseAction Copy()
        {
            var action = new GoToSchoolAction();
            Setup(action);

            return action;
        }

        public override bool CheckContext()
        {
            return true;
        }
    }
}