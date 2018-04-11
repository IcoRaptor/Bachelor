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
        private readonly AStarStorage _storage;

        private volatile bool _running;

        #endregion

        #region Properties

        public AStarResult Result { get; private set; }

        #endregion

        #region Constructors

        public AStarSolver(AStarParams param)
        {
            _goal = param.Goal;
            _map = param.Map;

            _storage = new AStarStorage();
            Result = new AStarResult();

            _running = true;
        }

        #endregion

        public void Solve()
        {
            // 1. Let P = starting node
            // 2. Assign f, g, h to P
            // 3. Add P to open list
            // 4. Let B = node from open list with lowest f value
            //      a. If B is goal then quit
            //      b. If open list is empty quit
            // 5. Let C = a valid node connected to B
            //      a. Assign f, g, h to C
            //      b. Check wheter C is on a list
            //          i. If so, check if new path is better
            //          ii. Else add C to open list
            //      c. Repeat 5 for all valid children of B
            // 6. Repeat from step 4

            try
            {
                // Create start node and add it to the open list
                var p = _map.CreateNewNode(_goal, "root");
                _storage.AddNodeToOpenList(p);

                // Iterate the open list
                while (!_storage.OpenListEmpty)
                {
                    // Get the node with lowest f value from open list
                    var b = _storage.GetBestNode();

                    // Add to plan
                    Result.Nodes.AddLast(b);

                    // Stop if this is the goal node
                    if (_goal.IsGoalNode(b))
                    {
                        Result.Code = RETURN_CODE.SUCCESS;
                        break;
                    }

                    // Get all neighbours connected to the node
                    var neighbours = _map.GetNeighbours(b);

                    // Iterate neighbours
                    foreach (var c in neighbours)
                    {
                        // Add to open list if necessary
                        if (!c.OnOpenList && !c.OnClosedList)
                            _storage.AddNodeToOpenList(c);

                        // Find the one with the lowest f value and add to plan
                    }

                    _storage.AddNodeToClosedList(b);
                }

                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Debugger.Log(LOG_TYPE.ERROR,
                    e.ToString());

                Result.Code = RETURN_CODE.ERROR;
            }
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