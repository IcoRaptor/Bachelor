namespace AI.GOAP
{
    public class WorkAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new WorkAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}