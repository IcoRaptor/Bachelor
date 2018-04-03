using AI.GOAP;

namespace AStar
{
    /// <summary>
    /// Represents a node in the planning graph
    /// </summary>
    public class AStarNodePlanning : AStarNode
    {
        #region Properties

        public WorldState State { get; private set; }

        #endregion

        public AStarNodePlanning(WorldState state,
            float g, float h, AStarNode root = null)
            : base(g, h, root)
        {
            State = state;
        }
    }
}