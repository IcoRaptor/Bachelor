namespace AI.GOAP
{
    public abstract class BaseAction
    {
        #region Variables

        protected STATE_SYMBOL[] _preconditions;
        protected STATE_SYMBOL[] _effects;

        protected int _cost;
        protected uint _timeInMinutes;

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