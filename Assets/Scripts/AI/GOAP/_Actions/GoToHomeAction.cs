namespace AI.GOAP
{
    public class GoToHomeAction : GoToAction
    {
        public override void Activate(AIModule module)
        {
            _target = module.Memory.Home;
            base.Activate(module);
        }

        public override BaseAction Copy()
        {
            var action = new GoToHomeAction();
            Setup(action);

            return action;
        }

        public override bool CheckContext()
        {
            return true;
        }
    }
}