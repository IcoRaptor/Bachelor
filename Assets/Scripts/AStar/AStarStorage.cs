using System.Collections.Generic;

namespace AStar
{
    public class AStarStorage
    {
        #region Variables

#pragma warning disable 0414
        private List<AStarNode> _openList = new List<AStarNode>();
        private List<AStarNode> _closedList = new List<AStarNode>();
#pragma warning restore

        #endregion
    }
}