namespace AStar
{
    public class AStarParams
    {
        #region Properties

        public AStarGoal Goal { get; set; }
        public AStarMap Map { get; set; }
        public AStarCallback Callback { get; set; }

        #endregion
    }
}