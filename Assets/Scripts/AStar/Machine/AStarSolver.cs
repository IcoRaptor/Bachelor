using Framework.Debugging;
using System;

namespace AStar
{
    /// <summary>
    /// Helper class that solves the A* problem
    ///  (IDisposable)
    /// </summary>
    internal class AStarSolver : IDisposable
    {
        #region Variables

        private const int _MAX_TIME = 2 * 1000; // 2 seconds

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

        public void Dispose()
        {
            _running = false;

            _stopwatch.Stop();
            Debugger.LogFormat(LOG_TYPE.LOG,
                "Solver finished\n{0} ms",
                _stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Is the execution complete
        /// </summary>
        public bool Finished()
        {
            return _running == false;
        }

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
                // Stop if the search is taking too long
                if (_stopwatch.ElapsedMilliseconds >= _MAX_TIME)
                {
                    Debugger.Log(LOG_TYPE.WARNING,
                        "Solver: Execution expired...\n");
                    break;
                }

                // Get node with lowest f value from open list
                var node = _storage.GetBestNode();

                // Stop if this is the goal node
                if (_goal.CheckNode(node))
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

        private void HandleGoalNode(AStarNode goalNode)
        {
            // Set solution
            _storage.AddNodeToClosedList(goalNode);
            goalNode.SolutionNode = true;

            Result.Code = RETURN_CODE.SUCCESS;
            Result.Nodes = _storage.GetFinalPath();
        }

        private void ProcessNeighbours(AStarNode root)
        {
            // Get all neighbours connected to the node
            var neighbours = _map.GetNeighbours(root);

            foreach (var current in neighbours)
            {
                // Do nothing if node is on closed list
                if (current.OnClosedList)
                    continue;

                // Evaluate initial node
                if (!current.OnOpenList)
                {
                    HandleInitialNode(current, root);
                    continue;
                }

                // Evaluate visited node
                HandleVisitedNode(current, root);
            }
        }

        private void HandleInitialNode(AStarNode current, AStarNode root)
        {
            // Setup node
            current.Root = root;
            current.G = root.G + current.Cost;
            current.Priority = current.G + current.H;

            _storage.AddNodeToOpenList(current);
        }

        private void HandleVisitedNode(AStarNode current, AStarNode root)
        {
            // Do nothing if the old path is better
            if (current.G <= root.G + current.Cost)
                return;

            // Update path
            current.G = root.G + current.Cost;
            current.Root = root;

            current.Update(_storage);
        }
    }
}