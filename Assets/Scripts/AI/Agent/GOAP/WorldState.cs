namespace AI.GOAP
{
    /// <summary>
    /// Represents an agents world state
    /// </summary>
    public sealed class WorldState
    {
        #region Variables

        private STATE[] _symbols;

        #endregion

        #region Constructors

        public WorldState(int num)
        {
            _symbols = new STATE[num];
        }

        #endregion

        public STATE[] GetSymbols()
        {
            return _symbols;
        }

        /// <summary>
        /// Returns the number of unsatisfied symbols
        /// </summary>
        public int GetNumberOfUnsatisfiedSymbols()
        {
            int num = 0;

            foreach (var symbol in _symbols)
                if (symbol == STATE.UNSATISFIED)
                    ++num;

            return num;
        }

        /// <summary>
        /// Returns the number of differences between the states
        /// </summary>
        public int GetSymbolDifference(WorldState other)
        {
            int num = 0;
            STATE[] otherSymbols = other.GetSymbols();

            for (int i = 0; i < _symbols.Length; i++)
                if (_symbols[i] != otherSymbols[i])
                    ++num;

            return num;
        }

        #region Operators

        public static bool operator ==(WorldState a, WorldState b)
        {
            return false;
        }

        public static bool operator !=(WorldState a, WorldState b)
        {
            return true;
        }

        #endregion

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public enum STATE
    {
        UNSET = -1,
        SATISFIED,
        UNSATISFIED
    }
}