namespace AI.GOAP
{
    /// <summary>
    /// Represents an agents world state
    /// </summary>
    public sealed class WorldState : IGOAPImmutable<WorldState>
    {
        #region Variables

        public static readonly int SymbolCount = GOAPResolver.SymbolCount;

        #endregion

        #region Properties

        public STATE_SYMBOL[] Symbols { get; private set; }

        #endregion

        #region Constructors

        public WorldState()
        {
            Init();
        }

        public WorldState(STATE_SYMBOL[] symbols)
        {
            if (symbols.Length != SymbolCount)
            {
                Init();
                return;
            }

            Symbols = new STATE_SYMBOL[SymbolCount];

            for (int i = 0; i < SymbolCount; i++)
                Symbols[i] = symbols[i];
        }

        #endregion

        /// <summary>
        /// Returns the number of differences between the states
        /// </summary>
        public int GetSymbolDifference(WorldState other)
        {
            int num = 0;
            STATE_SYMBOL[] otherSymbols = other.Symbols;

            for (int i = 0; i < Symbols.Length; i++)
                if (Symbols[i] != otherSymbols[i])
                    ++num;

            return num;
        }

        public bool Satisfies(WorldState other)
        {
            STATE_SYMBOL[] symbols = other.Symbols;

            if (Symbols.Length != symbols.Length)
                return false;

            for (int i = 0; i < Symbols.Length; i++)
            {
                if (Symbols[i] == STATE_SYMBOL.UNSET ||
                    symbols[i] == STATE_SYMBOL.UNSET)
                {
                    continue;
                }

                if (Symbols[i] != symbols[i])
                    return false;
            }

            return true;
        }

        public WorldState Copy()
        {
            return new WorldState(Symbols);
        }

        private void Init()
        {
            Symbols = new STATE_SYMBOL[SymbolCount];

            for (var i = 0; i < SymbolCount; i++)
                Symbols[i] = STATE_SYMBOL.UNSET;
        }

        public override string ToString()
        {
            string s = string.Empty;

            foreach (var symbol in Symbols)
                s += symbol.ToString() + " ";

            return s;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Symbols.GetHashCode();
        }
    }
}