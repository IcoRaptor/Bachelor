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
        private RETURN_CODE _code = RETURN_CODE.DEFAULT;

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
        ///  Calls OnAStarFinished
        /// </summary>
        public void Update()
        {
            lock (_lock)
            {
                if (_Finished)
                {
                    if (_callback != null)
                        _callback(_code);

                    _code = RETURN_CODE.DEFAULT;
                }

                _wasRunning = _running;
            }
        }

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public bool RunAStar(AStarGoal goal, AStarCallback callback)
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
                    if (goal == null)
                        throw new ArgumentNullException("AStarGoal");

                    if (!ThreadPool.QueueUserWorkItem(e => Run(goal)))
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
        private void Run(AStarGoal goal)
        {
            var solver = new AStarSolver(goal);
            RETURN_CODE code = solver.Solve();

            Terminate(code);
        }

        /// <summary>
        /// Exit function for Run
        /// </summary>
        private void Terminate(RETURN_CODE code)
        {
            lock (_lock)
            {
                _code = code;
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
        private readonly AStarStorage _storage;

        #endregion

        public AStarSolver(AStarGoal goal)
        {
            _goal = goal;
            _storage = new AStarStorage();
        }

        public RETURN_CODE Solve()
        {
            try
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

                // 1-3:
                var p = _goal.GetStartNode();
                _storage.AddNodeToOpenList(p);

                // 6:
                while (!_storage.OpenListEmpty)
                {
                    // 4:
                    var b = _storage.GetNextBestNode();

                    if (_goal.IsGoalNode(b))
                        break;

                    // 5: TODO
                }

                // Test, waits for 2 seconds
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Debugger.Log(LOG_TYPE.ERROR,
                    e.ToString());

                return RETURN_CODE.ERROR;
            }

            return RETURN_CODE.SUCCESS;
        }
    }
}