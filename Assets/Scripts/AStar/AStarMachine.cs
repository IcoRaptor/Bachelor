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
        public void RunAStar(AStarParams param)
        {
            lock (_lock)
            {
                if (_running)
                {
                    _aStarQueue.Enqueue(param);
                    return;
                }

                try
                {
                    if (!ThreadPool.QueueUserWorkItem(e => Run(param)))
                        throw new Exception("Item could not be queued!");

                    _callback = param.Callback;
                    _running = true;
                }
                catch (Exception e)
                {
                    PrintError(param.Callback, e);
                    _aStarQueue.Enqueue(param);
                }
            }
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarParams param)
        {
            var solver = new AStarSolver(param);
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
        private void PrintError(AStarCallback callback, Exception e)
        {
            bool callbackNull = callback == null;

            string cb = callbackNull ?
                "No callback defined" :
                callback.Method.Name;

            string go = callbackNull ?
                string.Empty :
                ((MonoBehaviour)callback.Target).transform.parent.name;

            if (callbackNull)
                Debugger.Log(LOG_TYPE.WARNING, e.Message + "\n" + cb);
            else
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "{0}\nCallback: {1}, GO: {2}",
                    e.Message, cb, go);
            }
        }
    }
}