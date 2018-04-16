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

        public string ID { get; protected set; }

        /// <summary>
        /// The root of this node
        /// </summary>
        public AStarNode Root { get; set; }

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

        /// <summary>
        /// Is this node part of the solution
        /// </summary>
        public bool PartOfSolution { get; private set; }

        #endregion

        /// <summary>
        /// Processes the solution path backwards
        /// </summary>
        public void ProcessFinalPath()
        {
            PartOfSolution = true;

            if (Root == null)
                return;

            Root.ProcessFinalPath();
        }

        /// <summary>
        /// Call this after updating G or H
        /// </summary>
        public void Update(AStarStorage storage)
        {
            storage.UpdateOpenList(this, G + H);
        }
    }
}