using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Interface for A* storages
    /// </summary>
    public interface IAStarStorage
    {
        #region Properties

        bool OpenListEmpty { get; }

        #endregion

        bool AddNodeToOpenList(AStarNode node);

        bool AddNodeToClosedList(AStarNode node);

        AStarNode GetBestNode();

        LinkedList<AStarNode> GetFinalPath();

        void UpdateOpenList(AStarNode node, float f);
    }
}