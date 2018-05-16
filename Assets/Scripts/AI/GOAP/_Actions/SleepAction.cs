namespace AI.GOAP
{
    public class SleepAction : BaseAction
    {
        public override bool CheckContext()
        {
            uint hour = TimeManager.Instance.GetTimeStamp().Hours;
            return hour > 21;
        }

        public override BaseAction Copy()
        {
            var action = new SleepAction();
            Setup(action);

            return action;
        }

        public override void Update(AIModule module)
        {
        }
    }
}