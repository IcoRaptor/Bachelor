namespace AI.GOAP
{
    public class PatrolAction : GoToAction
    {
        public override bool CheckContext()
        {
            return true;
        }

        public override void Update(AIModule module)
        {
        }
    }
}