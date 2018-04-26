using System.Collections.Generic;

namespace AI.GOAP
{
    public abstract class BaseAction : IGOAPImmutable<BaseAction>, IEqualityComparer<BaseAction>
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

        public abstract bool Validate();

        public virtual WorldState ApplyEffects(WorldState state)
        {
            var temp = state.Copy();

            for (int i = 0; i < WorldState.NUM_SYMBOLS; i++)
            {
                if (Effects.Symbols[i] != STATE_SYMBOL.SATISFIED)
                    continue;

                if (Effects.Symbols[i] != STATE_SYMBOL.UNSET)
                    temp.Symbols[i] = Effects.Symbols[i];
            }

            return temp;
        }

        public virtual WorldState ApplyPreconditions(WorldState state)
        {
            var temp = state.Copy();

            for (int i = 0; i < WorldState.NUM_SYMBOLS; i++)
            {
                if (Preconditions.Symbols[i] != STATE_SYMBOL.UNSATISFIED)
                    continue;

                if (Preconditions.Symbols[i] != STATE_SYMBOL.UNSET)
                    temp.Symbols[i] = Preconditions.Symbols[i];
            }

            return temp;
        }

        public virtual bool CheckPreconditions(WorldState state)
        {
            for (int i = 0; i < WorldState.NUM_SYMBOLS; i++)
            {
                var s = Preconditions.Symbols[i];

                if (s == STATE_SYMBOL.UNSET)
                    continue;

                if (s != state.Symbols[i])
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

        public virtual bool IsComplete()
        {
            return _complete;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseAction))
                return false;

            var action = (BaseAction)obj;

            return string.CompareOrdinal(ID, action.ID) == 0;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public bool Equals(BaseAction x, BaseAction y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(BaseAction obj)
        {
            return obj.GetHashCode();
        }
    }
}