using Framework.Debugging;
using System;
using System.Threading;

namespace AStar
{
    internal class AStarSolver : IDisposable
    {
        #region Variables

        private readonly AStarGoal _goal;
        private readonly AStarMap _map;
        private readonly IAStarStorage _storage;

        private volatile bool _running;

        #endregion

        #region Properties

        public AStarResult Result { get; private set; }

        #endregion

        #region Constructors

        public AStarSolver(AStarParams asp)
        {
            _storage = asp.Storage ?? new AStarStorage();

            _goal = asp.Goal;
            _map = asp.Map;

            Result = new AStarResult();

            _running = true;
        }

        #endregion

        public void Solve()
        {
            // Test
            Thread.Sleep(2000);

            try
            {
                CreateStartNode();
                ProcessOpenList();
            }
            catch (Exception e)
            {
                Debugger.Log(LOG_TYPE.ERROR,
                    e.ToString());

                Result.Code = RETURN_CODE.ERROR;
            }
        }

        private void CreateStartNode()
        {
            var root = _map.CreateNewNode(_goal, Strings.ROOT_NODE);
            _storage.AddNodeToOpenList(root);
        }

        private void ProcessOpenList()
        {
            while (!_storage.OpenListEmpty)
            {
                // Get node with lowest f value from open list
                var node = _storage.GetBestNode();

                // Stop if this is the goal node
                if (_goal.IsGoalNode(node))
                {
                    HandleGoalNode(node);
                    break;
                }

                ProcessNeighbours(node);

                // Add node to closed list
                _storage.AddNodeToClosedList(node);
            }

            if (Result.Code == RETURN_CODE.DEFAULT)
                Result.Code = RETURN_CODE.NO_PATH_FOUND;
        }

        private void HandleGoalNode(AStarNode node)
        {
            Result.Code = RETURN_CODE.SUCCESS;

            node.ProcessFinalPath();

            // TODO: Fill result nodes
            Result.Nodes = _storage.GetFinalPath();
        }

        private void ProcessNeighbours(AStarNode node)
        {
            // Get all neighbours connected to the node
            var neighbours = _map.GetNeighbours(node);

            foreach (var n in neighbours)
            {
                // Add to open list if necessary
                if (!n.OnOpenList && !n.OnClosedList)
                {
                    // Fill in g and h

                    _storage.AddNodeToOpenList(n);
                    continue;
                }

                // Do stuff with costs and backpropagation
            }
        }

        private void InitialNodeSetup(AStarNode node)
        {
        }

        private void VisitedNodeSetup(AStarNode node)
        {
        }

        public void Dispose()
        {
            _running = false;
        }

        public bool IsFinished()
        {
            return _running == false;
        }
    }
}