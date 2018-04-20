using Framework.Debugging;
using System;

namespace AStar
{
    /// <summary>
    /// Helper class that solves the A* problem.
    ///  Implements IDisposable
    /// </summary>
    internal class AStarSolver : IDisposable
    {
        #region Variables

        private readonly AStarGoal _goal;
        private readonly AStarMap _map;
        private readonly IAStarStorage _storage;

        private volatile bool _running;

        private System.Diagnostics.Stopwatch _stopwatch;

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
            _stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        #endregion

        /// <summary>
        /// Executes the A* algorithm
        /// </summary>
        public void Solve()
        {
            try
            {
                // Create root node
                var root = _map.CreateRootNode();
                _storage.AddNodeToOpenList(root);

                // Process the open list
                ProcessOpenList();

                // Was a path found?
                if (Result.Code == RETURN_CODE.DEFAULT)
                    Result.Code = RETURN_CODE.NO_PATH_FOUND;
            }
            catch (Exception e)
            {
                Debugger.Log(LOG_TYPE.ERROR,
                    e.ToString());

                Result.Code = RETURN_CODE.ERROR;
            }
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

                // Go through all neighbours
                ProcessNeighbours(node);

                // Add node to closed list
                _storage.AddNodeToClosedList(node);
            }
        }

        private void HandleGoalNode(AStarNode node)
        {
            _storage.AddNodeToClosedList(node);
            node.SolutionNode = true;

            Result.Code = RETURN_CODE.SUCCESS;
            Result.Nodes = _storage.GetFinalPath();
        }

        private void ProcessNeighbours(AStarNode node)
        {
            // Get all neighbours connected to the node
            var neighbours = _map.GetNeighbours(node);

            foreach (var n in neighbours)
            {
                // Do nothing if node is on closed list
                if (n.OnClosedList)
                    continue;

                // Evaluate initial node
                if (!n.OnOpenList)
                {
                    HandleInitialNode(n, node);
                    continue;
                }

                // Evaluate visited node
                HandleVisitedNode(n, node);
            }
        }

        private void HandleInitialNode(AStarNode current, AStarNode root)
        {
            current.Root = root;
            current.G = root.G + current.Cost;
            current.Priority = current.G + current.H;

            _storage.AddNodeToOpenList(current);
        }

        private void HandleVisitedNode(AStarNode current, AStarNode root)
        {
            if (current.G <= root.G + current.Cost)
                return;

            current.G = root.G + current.Cost;
            current.Root = root;

            current.Update(_storage);
        }

        public void Dispose()
        {
            _running = false;

            _stopwatch.Stop();
            Debugger.LogFormat(LOG_TYPE.LOG,
                "Elapsed milliseconds: {0}\nTicks: {1}",
                _stopwatch.ElapsedMilliseconds, _stopwatch.ElapsedTicks);
        }

        /// <summary>
        /// Is the execution complete
        /// </summary>
        public bool Finished()
        {
            return _running == false;
        }
    }
}