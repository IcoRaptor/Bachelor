﻿namespace AI.GOAP
{
    public abstract class BaseAction
    {
        #region Variables

        protected bool _complete;
        protected bool _active;

        #endregion

        #region Properties

        public string ID { get; set; }

        public string Dialog { get; set; }

        public int Cost { get; set; }

        public int TimeInMinutes { get; set; }

        public WorldState Preconditions { get; set; }

        public WorldState Effects { get; set; }

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

                s = Effects.Symbols[i];
            }

            return oldState;
        }

        public virtual bool CheckPreconditions(WorldState current)
        {
            var symbols = current.Symbols;

            for (int i = 0; i < symbols.Length; i++)
            {
                var s = Preconditions.Symbols[i];

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