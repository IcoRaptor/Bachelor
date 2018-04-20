using AI.GOAP;

namespace AStar
{
    /// <summary>
    /// Represents a node in the planning graph
    /// </summary>
    public class AStarNodePlanning : AStarNode
    {
        #region Properties

        public WorldState Current { get; set; }

        #endregion

        #region Constructors

        public AStarNodePlanning(string id)
        {
            ID = id;
        }

        #endregion
    }
}