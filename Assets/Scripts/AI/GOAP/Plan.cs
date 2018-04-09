using System.Collections.Generic;

namespace AI.GOAP
{
    /// <summary>
    /// Represents a list of valid actions
    /// </summary>
    public class Plan
    {
        #region Variables

#pragma warning disable 0414
        private LinkedList<BaseAction> _plan =
            new LinkedList<BaseAction>();
#pragma warning restore

        #endregion
    }
}