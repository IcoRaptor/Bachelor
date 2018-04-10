using Framework.Debugging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AStar
{
    internal class AStarSolver
    {
        #region Variables

        private readonly AStarGoal _goal;
        private readonly AStarMap _map;
        private readonly AStarStorage _storage;

        private AStarResult _result;

        #endregion

        #region Constructors

        public AStarSolver(AStarParams param)
        {
            _goal = param.Goal;
            _map = param.Map;

            _storage = new AStarStorage();
            _result = new AStarResult()
            {
                Code = RETURN_CODE.DEFAULT,
                Nodes = new LinkedList<AStarNode>()
            };
        }

        #endregion

        public AStarResult Solve()
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
                    _result.Nodes.AddLast(b);

                    // Stop if this is the goal node
                    if (_goal.IsGoalNode(b))
                    {
                        _result.Code = RETURN_CODE.SUCCESS;
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

                _result.Code = RETURN_CODE.ERROR;
            }

            return _result;
        }
    }
}