namespace AI.GOAP
{
    public abstract class BaseAction
    {
        #region Variables

        protected STATE_SYMBOL[] _preconditions;
        protected STATE_SYMBOL[] _effects;

        protected bool _complete;
        protected bool _active;

        #endregion

        #region Properties

        public string ID { get; set; }

        public string Dialog { get; protected set; }

        public int Cost { get; protected set; }

        #endregion

        public abstract BaseAction Copy();

        public abstract void Update(AIModule module);

        public abstract bool CheckContext();

        public abstract bool IsValid();

        public virtual WorldState ApplyEffects(WorldState oldState)
        {
            var symbols = oldState.Symbols;

            for (int i = 0; i < symbols.Length; i++)
            {
                var s = oldState.Symbols[i];

                if (s == STATE_SYMBOL.UNSET)
                    continue;

                s = _effects[i];
            }

            return oldState;
        }

        public virtual bool CheckPreconditions(WorldState current)
        {
            var symbols = current.Symbols;

            for (int i = 0; i < symbols.Length; i++)
            {
                var s = _preconditions[i];

                if (s == STATE_SYMBOL.UNSET)
                    continue;

                if (s != symbols[i])
                    return false;
            }

            return true;
        }

        public virtual void Activate()
        {
            _active = true;
        }

        public virtual void Deactivate()
        {
            _active = false;
        }

        public virtual bool IsActionComplete()
        {
            return _complete;
        }
    }
}