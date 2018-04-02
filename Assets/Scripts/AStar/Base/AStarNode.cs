namespace AStar
{
    /// <summary>
    /// Represents a node on the AStarMap
    /// </summary>
    public abstract class AStarNode
    {
        #region Variables

        private bool _onOpenList = false;
        private bool _onClosedList = false;

        #endregion

        #region Properties

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
    }
}