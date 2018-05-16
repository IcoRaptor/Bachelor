namespace AI.GOAP
{
    /// <summary>
    /// Represents an agents world state
    /// </summary>
    public sealed class WorldState : IGOAPImmutable<WorldState>
    {
        #region Variables

        public static readonly int SymbolCount = GOAPResolver.StateCount;

        private STATE_SYMBOL[] _symbols;

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

            _symbols = new STATE_SYMBOL[SymbolCount];

            for (int i = 0; i < SymbolCount; i++)
                _symbols[i] = symbols[i];
        }

        #endregion

        #region Indexer

        public STATE_SYMBOL this[int i]
        {
            get { return _symbols[i]; }
            set { _symbols[i] = value; }
        }

        #endregion

        public int GetSymbolDifference(WorldState other)
        {
            int num = 0;

            for (int i = 0; i < SymbolCount; i++)
                if (this[i] != other[i])
                    ++num;

            return num;
        }

        public bool Satisfies(WorldState other)
        {
            for (int i = 0; i < SymbolCount; i++)
            {
                if (this[i] == STATE_SYMBOL.UNSET ||
                    other[i] == STATE_SYMBOL.UNSET)
                {
                    continue;
                }

                if (this[i] != other[i])
                    return false;
            }

            return true;
        }

        public WorldState Copy()
        {
            return new WorldState(_symbols);
        }

        private void Init()
        {
            _symbols = new STATE_SYMBOL[SymbolCount];

            for (var i = 0; i < SymbolCount; i++)
                this[i] = STATE_SYMBOL.UNSET;
        }

        public override string ToString()
        {
            string s = string.Empty;

            foreach (var symbol in _symbols)
                s += symbol.ToString() + " ";

            return s;
        }
    }
}