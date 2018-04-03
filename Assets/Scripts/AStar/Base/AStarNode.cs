using Priority_Queue;

namespace AStar
{
    /// <summary>
    /// Represents a node on the AStarMap
    /// </summary>
    public abstract class AStarNode : FastPriorityQueueNode
    {
        #region Variables

        private bool _onOpenList = false;
        private bool _onClosedList = false;

        #endregion

        #region Properties

        public int ID { get; private set; }

        /// <summary>
        /// The root of this node
        /// </summary>
        public AStarNode Root { get; set; }

        /// <summary>
        /// Fitness (G + H)
        /// </summary>
        public float F { get { return Priority; } }

        /// <summary>
        /// Cost from start node
        /// </summary>
        public float G { get; set; }

        /// <summary>
        /// Heuristic (Cost to goal)
        /// </summary>
        public float H { get; set; }

        /// <summary>
        /// Is this node on the open list?
        /// </summary>
        public bool OnOpenList
        {
            get { return _onOpenList; }
            set
            {
                _onOpenList = value;

                if (_onOpenList)
                    _onClosedList = false;
            }
        }

        /// <summary>
        /// Is this node on the closed list?
        /// </summary>
        public bool OnClosedList
        {
            get { return _onClosedList; }
            set
            {
                _onClosedList = value;

                if (_onClosedList)
                    _onOpenList = false;
            }
        }

        #endregion

        #region Constructors

        public AStarNode(float g, float h, AStarNode root = null)
        {
            Root = root;

            G = g;
            H = h;
            Priority = G + H;
        }

        #endregion

        public void Update(float g, float h, AStarStorage storage)
        {
            G = g;
            H = h;

            storage.UpdateLists(this, G + H);
        }
    }
}