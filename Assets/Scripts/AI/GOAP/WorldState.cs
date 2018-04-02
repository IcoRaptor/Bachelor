﻿namespace AI.GOAP
{
    /// <summary>
    /// Represents an agents world state
    /// </summary>
    public sealed class WorldState
    {
        #region Properties

        public STATE_SYMBOL[] Symbols { get; private set; }

        #endregion

        #region Constructors

        public WorldState(int num)
        {
            Symbols = new STATE_SYMBOL[num];
        }

        #endregion

        /// <summary>
        /// Returns the number of unsatisfied symbols
        /// </summary>
        public int GetNumberOfUnsatisfiedSymbols()
        {
            int num = 0;

            foreach (var symbol in Symbols)
                if (symbol == STATE_SYMBOL.UNSATISFIED)
                    ++num;

            return num;
        }

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

        #region Operators

        public static bool operator ==(WorldState a, WorldState b)
        {
            STATE_SYMBOL[] aSymbols = a.Symbols;
            STATE_SYMBOL[] bSymbols = b.Symbols;

            if (aSymbols.Length != bSymbols.Length)
                return false;

            for (int i = 0; i < aSymbols.Length;)
                if (aSymbols[i] != bSymbols[i])
                    return false;

            return true;
        }

        public static bool operator !=(WorldState a, WorldState b)
        {
            STATE_SYMBOL[] aSymbols = a.Symbols;
            STATE_SYMBOL[] bSymbols = b.Symbols;

            if (aSymbols.Length != bSymbols.Length)
                return true;

            for (int i = 0; i < aSymbols.Length;)
                if (aSymbols[i] != bSymbols[i])
                    return true;

            return false;
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
}