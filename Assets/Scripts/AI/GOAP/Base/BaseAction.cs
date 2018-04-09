namespace AI.GOAP
{
    public abstract class BaseAction
    {
        #region Variables

        protected STATE_SYMBOL[] _preconditions;
        protected STATE_SYMBOL[] _effects;

        protected uint _timeInMinutes;

        #endregion

        #region Properties

        public string ID { get; private set; }

        public int Cost { get; private set; }

        #endregion

        public abstract bool TestState(WorldState state);

        public abstract void ApplyEffects(WorldState state);

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract bool CheckContext();

        public abstract bool IsActionComplete();

        public abstract bool IsValid();
    }
}