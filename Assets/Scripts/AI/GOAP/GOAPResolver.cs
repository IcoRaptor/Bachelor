using Framework.Debugging;
using System.Collections.Generic;

namespace AI.GOAP
{
    /// <summary>
    /// Symbol to index mapping for WorldState and Discontentment
    /// </summary>
    public static class GOAPResolver
    {
        #region Variables

        private static Dictionary<string, int> _symbolToStateIndex =
            new Dictionary<string, int>()
            {
                { Strings.STATE_WORKING, 0 },
                { Strings.STATE_AT_HOME, 1 },
                { Strings.STATE_RESTED, 2 },
                { Strings.STATE_FULL, 3 },
                { Strings.STATE_HAPPY, 4 },
                { Strings.STATE_AT_WORK, 5 },
                { Strings.STATE_AT_STORE, 6 },
                { Strings.STATE_HAS_FOOD, 7 },
                { Strings.STATE_HAS_MONEY, 8 },
                { Strings.STATE_AT_BAR, 9 },
                { Strings.STATE_AT_SCHOOL, 10 },
                { Strings.STATE_AT_PLAYGROUND, 11 },
            };

        private static int[] _indexOfMovementValues =
        {
            1, 5, 6, 9, 10, 11
        };

        private static Dictionary<string, int> _symbolToRelevanceIndex =
            new Dictionary<string, int>()
            {
                { Strings.DISC_FUN, 0 },
                { Strings.DISC_HUNGER, 1 },
                { Strings.DISC_MONEY, 2 },
                { Strings.DISC_SLEEP, 3 }
            };

        #endregion

        #region Properties

        public static int StateCount
        {
            get { return _symbolToStateIndex.Count; }
        }

        public static int RelevanceCount
        {
            get { return _symbolToRelevanceIndex.Count; }
        }

        #endregion

        public static int GetIndexFromSymbol(string id, RESOLVE resolve)
        {
            try
            {
                switch (resolve)
                {
                    case RESOLVE.WORLD_STATE:
                        return _symbolToStateIndex[id];

                    case RESOLVE.DISCONTENTMENT:
                        return _symbolToRelevanceIndex[id];
                }
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "Symbol '{0}' doesn't exist!\n",
                    id);
            }

            return -1;
        }

        public static bool GetMovementActionFromIndex(int index)
        {
            foreach (var i in _indexOfMovementValues)
                if (i == index)
                    return true;

            return false;
        }
    }
}