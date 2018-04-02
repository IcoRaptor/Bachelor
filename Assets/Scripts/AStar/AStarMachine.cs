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
                    if (goal == null)
                        throw new ArgumentNullException("AStarGoal");

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
            var callbackNull = callback == null;

            var cb = callbackNull ?
                "No callback defined" :
                callback.Method.Name;

            var go = callbackNull ?
                string.Empty :
                ((MonoBehaviour)callback.Target).transform.parent.name;

            var ex = e == null ?
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

#pragma warning disable 0414
        private readonly AStarGoal _goal;
        private readonly AStarMap _map;
        private readonly AStarStorage _storage;
#pragma warning restore

        #endregion

        public AStarSolver(AStarGoal goal, AStarMap map)
        {
            _goal = goal;
            _map = map;
            _storage = new AStarStorage();
        }

        public RETURN_CODE Solve()
        {
#if UNITY_EDITOR
            try
            {
#endif
                /*_storage.AddNodeToOpenList(new GOAPNode());
                var start = _storage.GetNextBestNode();*/

                // Test, waits for 2 seconds
                Thread.Sleep(2000);
#if UNITY_EDITOR
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return RETURN_CODE.ERROR;
            }
#endif

            return RETURN_CODE.SUCCESS;
        }
    }
}