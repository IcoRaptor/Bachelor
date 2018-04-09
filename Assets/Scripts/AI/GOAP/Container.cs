using Framework.Debugging;
using System.Collections.Generic;

namespace AI.GOAP
{
    /// <summary>
    /// Contains the goals and actions
    /// </summary>
    public static class Container
    {
        #region Variables

        private static Dictionary<string, BaseAction> _actionCache =
            new Dictionary<string, BaseAction>();

        #endregion

        /// <summary>
        /// Adds an action to the action cache
        /// </summary>
        public static void AddAction(BaseAction action)
        {
            if (!_actionCache.ContainsKey(action.ID))
                _actionCache.Add(action.ID, action);
        }

        /// <summary>
        /// Returns the action with the given ID.
        ///  May return null
        /// </summary>
        public static BaseAction GetAction(string id)
        {
            try
            {
                return _actionCache[id];
            }
            catch
            {
                if (string.CompareOrdinal(id, "root") != 0)
                {
                    Debugger.LogFormat(LOG_TYPE.WARNING,
                        "GetAction: ID '{0}' is doesn't exist!\n",
                        id);
                }

                return null;
            }
        }
    }
}