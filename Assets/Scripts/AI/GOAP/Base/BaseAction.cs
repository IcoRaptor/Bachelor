using System.Collections.Generic;
using UnityEngine;

namespace AI.GOAP
{
    public abstract class BaseAction :
        IGOAPImmutable<BaseAction>,
        IEqualityComparer<BaseAction>
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

        public virtual bool Validate(WorldState current)
        {
            return CheckPreconditions(current);
        }

        public virtual WorldState ApplyEffects(WorldState state)
        {
            var temp = state.Copy();

            for (int i = 0; i < WorldState.SymbolCount; i++)
                if (Effects[i] != STATE_SYMBOL.UNSET)
                    temp[i] = Effects[i];

            return temp;
        }

        public virtual WorldState ApplyPreconditions(WorldState state)
        {
            var temp = state.Copy();

            for (int i = 0; i < WorldState.SymbolCount; i++)
                if (Preconditions[i] == STATE_SYMBOL.SATISFIED)
                    temp[i] = STATE_SYMBOL.SATISFIED;

            return temp;
        }

        public virtual bool CheckPreconditions(WorldState state)
        {
            for (int i = 0; i < WorldState.SymbolCount; i++)
            {
                var s = Preconditions[i];

                if (s == STATE_SYMBOL.UNSET)
                    continue;

                if (s != state[i])
                    return false;
            }

            return true;
        }

        public virtual bool CheckEffects(WorldState state)
        {
            for (int i = 0; i < WorldState.SymbolCount; i++)
            {
                var s = Effects[i];

                if (s == STATE_SYMBOL.UNSET)
                    continue;

                if (s != state[i])
                    return true;
            }

            return false;
        }

        public virtual void Activate(AIModule module)
        {
            _active = true;
            module.Board.Dialog = Dialog;

            Debug.LogFormat(
                "{0}, Cost: {1}, Time: {2}\n{3}",
                GetType().ToString().Split('.')[2],
                Cost,
                TimeInMinutes,
                Dialog);
        }

        public virtual void Deactivate(AIModule module)
        {
            _active = false;

            module.Board.Dialog = string.Empty;
        }

        public virtual bool IsComplete()
        {
            return _complete;
        }

        protected void Setup(BaseAction action)
        {
            action.ID = ID;
            action.Dialog = Dialog;
            action.Cost = Cost;
            action.TimeInMinutes = TimeInMinutes;
            action.Effects = Effects.Copy();
            action.Preconditions = Preconditions.Copy();
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