using System.Collections.Generic;

namespace AI.GOAP
{
    public static class ActionHolder
    {
        #region Variables

        private static Dictionary<string, Action> _actionCache =
            new Dictionary<string, Action>();

        #endregion

        public static void AddAction(Action action)
        {
            if (!_actionCache.ContainsKey(action.ID))
                _actionCache.Add(action.ID, action);
        }
    }
}