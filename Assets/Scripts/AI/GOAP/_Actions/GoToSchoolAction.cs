namespace AI.GOAP
{
    public class GoToSchoolAction : GoToAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new GoToSchoolAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}