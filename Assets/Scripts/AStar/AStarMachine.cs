using Framework.Debugging;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace AStar
{
    public class AStarMachine
    {
        #region Variables

        private readonly object _lock = new object();
        private bool _running = false;
        private bool _wasRunning = false;

        private AStarCallback _callback = null;
        private AStarResult _result;

        private Queue<AStarParams> _aStarQueue = new Queue<AStarParams>();

        #endregion

        #region Properties

        private bool _Finished
        {
            get { return !_running && (_wasRunning != _running); }
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

            if (_aStarQueue.Count > 0)
                RunAStar(_aStarQueue.Dequeue());
        }

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public bool RunAStar(AStarParams aSP)
        {
            lock (_lock)
            {
                if (_running)
                {
                    _aStarQueue.Enqueue(aSP);
                    return true;
                }

                try
                {
                    if (!ThreadPool.QueueUserWorkItem(e => Run(aSP)))
                        throw new Exception("Item could not be queued!");

                    _callback = aSP.Callback;
                    _running = true;
                }
                catch (Exception e)
                {
                    PrintError(aSP.Callback, e);
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarParams aSP)
        {
            var solver = new AStarSolver(aSP);
            var result = solver.Solve();

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
                "Item could not be queued!" :
                e.Message;

            if (callbackNull)
                Debugger.Log(LOG_TYPE.WARNING, ex + cb);
            else
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "{0}\nCallback: {1}, GO: {2}",
                    ex, cb, go);
            }
        }
    }
}