using Framework.Debugging;
using System;
using System.Threading;

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

        private volatile bool _interrupt;
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

            _interrupt = false;
            _running = true;
        }

        #endregion

        /// <summary>
        /// Executes the A* algorithm
        /// </summary>
        public void Solve()
        {
            // Test
            Thread.Sleep(2000);

            try
            {
                // Create root node
                var root = _map.CreateNewNode(_goal, Strings.ROOT_NODE);
                _storage.AddNodeToOpenList(root);

                // Process the open list
                ProcessOpenList();
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
                if (_interrupt)
                {
                    Result.Code = RETURN_CODE.INTERRUPT;
                    break;
                }

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
            _storage.AddNodeToClosedList(node);
            node.ProcessFinalPath();

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
                    HandleInitialNode(node, n);
                    continue;
                }

                // Evaluate visited node
                HandleVisitedNode(node, n);
            }
        }

        private void HandleInitialNode(AStarNode root, AStarNode current)
        {
            // Fill in g and h

            _storage.AddNodeToOpenList(current);
        }

        private void HandleVisitedNode(AStarNode root, AStarNode current)
        {
            // Evaluate g and h
        }

        public void Dispose()
        {
            _running = false;
        }

        /// <summary>
        /// Interrupts the execution
        /// </summary>
        public void Interrupt()
        {
            _interrupt = true;
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