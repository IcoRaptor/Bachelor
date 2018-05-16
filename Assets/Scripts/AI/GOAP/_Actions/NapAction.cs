namespace AI.GOAP
{
    public class NapAction : BaseAction
    {
        public override bool CheckContext()
        {
            uint hour = TimeManager.Instance.GetTimeStamp().Hours;
            return hour > 10 && hour < 16;
        }

        public override BaseAction Copy()
        {
            var action = new NapAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}