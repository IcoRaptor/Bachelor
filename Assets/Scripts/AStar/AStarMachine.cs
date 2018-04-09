using Framework.Debugging;
using System;
using System.Threading;
using UnityEngine;

namespace AStar
{
    public class AStarMachine
    {
        #region Variables

        private readonly object _lock = new object();

        private AStarCallback _callback = null;
        private AStarResult _result;

        private bool _running = false;
        private bool _wasRunning = false;
        private bool _hasRun = false;

        #endregion

        #region Properties

        private bool _Finished
        {
            get { return _hasRun && !_running && (_running != _wasRunning); }
        }

        #endregion

        /// <summary>
        /// Checks if an A* search has finished.
        ///  Calls the AStarCallback
        /// </summary>
        public void Update()
        {
            lock (_lock)
            {
                if (_Finished)
                {
                    if (_callback != null)
                        _callback(_result);

                    _result = new AStarResult();
                }

                _wasRunning = _running;
            }
        }

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public bool RunAStar(AStarGoal goal, AStarMap map, AStarCallback callback)
        {
            lock (_lock)
            {
                if (_hasRun && _running)
                {
                    PrintError(callback);
                    return false;
                }

                try
                {
                    if (goal == null || map == null)
                        throw new ArgumentNullException("Goal/Map");

                    if (!ThreadPool.QueueUserWorkItem(e => Run(goal, map)))
                        throw new Exception("Item could not be queued!");

                    _callback = callback;
                    _hasRun = true;
                    _running = true;
                }
                catch (Exception e)
                {
                    PrintError(callback, e);
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarGoal goal, AStarMap map)
        {
            var solver = new AStarSolver(goal, map);
            AStarResult result = solver.Solve();

            Terminate(result);
        }

        /// <summary>
        /// Exit function for Run
        /// </summary>
        private void Terminate(AStarResult result)
        {
            lock (_lock)
            {
                _result = result;
                _running = false;
            }
        }

        /// <summary>
        /// Prints an error message
        /// </summary>
        private void PrintError(AStarCallback callback, Exception e = null)
        {
            bool callbackNull = callback == null;

            string cb = callbackNull ?
                "No callback defined" :
                callback.Method.Name;

            string go = callbackNull ?
                string.Empty :
                ((MonoBehaviour)callback.Target).transform.parent.name;

            string ex = e == null ?
                "Item could not be queued!\n" :
                e.Message;

            if (callbackNull)
                Debugger.Log(LOG_TYPE.WARNING, ex + cb);
            else
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "{0}Callback: {1}, GO: {2}",
                    ex, cb, go);
            }
        }
    }

    internal class AStarSolver
    {
        #region Variables

        private readonly AStarGoal _goal;
        private readonly AStarMap _map;
        private readonly AStarStorage _storage;

        private AStarResult _result;

        #endregion

        #region Constructors

        public AStarSolver(AStarGoal goal, AStarMap map)
        {
            _goal = goal;
            _map = map;

            _storage = new AStarStorage();
            _result = new AStarResult();
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
                var p = _map.CreateNewNode(_goal, "root");
                _storage.AddNodeToOpenList(p);

                while (!_storage.OpenListEmpty)
                {
                    var b = _storage.GetBestNode();

                    if (_goal.IsGoalNode(b))
                    {
                        _result.Code = RETURN_CODE.SUCCESS;
                        break;
                    }

                    var neighbours = _map.GetNeighbours(b.ID);

                    foreach (var c in neighbours)
                    {
                        if (!c.OnOpenList && !c.OnClosedList)
                            _storage.AddNodeToOpenList(c);
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