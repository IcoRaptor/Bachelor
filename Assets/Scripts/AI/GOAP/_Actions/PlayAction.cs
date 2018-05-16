namespace AI.GOAP
{
    public class PlayAction : BaseAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override BaseAction Copy()
        {
            var action = new PlayAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}