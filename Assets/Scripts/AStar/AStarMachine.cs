using Framework.Debugging;
using System;
using System.Threading;

namespace AStar
{
    public class AStarMachine
    {
        #region Variables

        private object _lock = new object();

        private AStarCallback _onFinished = null;

        private bool _running = false;
        private bool _wasRunning = false;
        private bool _hasRun = false;

        #endregion

        #region Properties

        private bool _Finished
        {
            get
            {
                return !_running &&
                (_running != _wasRunning) &&
                _onFinished != null;
            }
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
                    _onFinished();

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
            }

            try
            {
                if (!ThreadPool.QueueUserWorkItem((e) => Run(goal, map)))
                    throw new Exception();

                lock (_lock)
                {
                    _onFinished = callback;
                    _hasRun = true;
                    _running = true;
                }
            }
            catch
            {
                PrintError(callback);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarGoal goal, AStarMap map)
        {
            // Test, waits for 1 second
            Thread.Sleep(1000);

            lock (_lock)
                _running = false;
        }

        /// <summary>
        /// Prints an error message
        /// </summary>
        private void PrintError(AStarCallback callback)
        {
            if (callback == null)
            {
                Debugger.Log(LOG_TYPE.WARNING,
                    "Item could not be queued!\n" +
                    "OnAStarFinished (null)");
            }
            else
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "Item could not be queued!\n{0}",
                    callback.Target.ToString());
            }
        }
    }

    public delegate void AStarCallback();
}