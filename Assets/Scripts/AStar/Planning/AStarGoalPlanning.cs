using AI.GOAP;

namespace AStar
{
    public class AStarGoalPlanning : AStarGoal
    {
        #region Variables

        private WorldState _target;
        private WorldState _current;

        #endregion

        #region Properties

        public BaseGoal Goal { get; private set; }

        #endregion

        #region Constructors

        public AStarGoalPlanning(BaseGoal goal)
        {
            Goal = goal;
            _target = goal.Target.Copy();
            _current = goal.Current.Copy();
        }

        #endregion

        public override void CalcValues(AStarNode node, out float g, out float h)
        {
            var action = Container.GetAction(node.ID);
            bool root = action == null;

            g = root ? 0 : action.Cost;
            h = _target.GetSymbolDifference(_current);

            UnityEngine.Debug.LogFormat("g: {0}\nh: {1}", g, h);
        }

        public override bool IsGoalNode(AStarNode node)
        {
            var castNode = node as AStarNodePlanning;

            return castNode.Current == _target;
        }

        public override bool Validate()
        {
            return false;
        }
    }
}