using Framework.Debugging;
using System;
using System.Threading;

namespace AStar
{
    public class AStarMachine
    {
        #region Variables

        private OnAStarFinished _onFinished = null;
        private volatile bool _running = false;
        private volatile bool _wasRunning = false;
        private bool _hasRun = false;

        #endregion

        #region Properties

        private bool _CanUpdate
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
            if (_CanUpdate)
                _onFinished();

            _wasRunning = _running;
        }

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public bool RunAStar(AStarGoal goal, AStarMap map, OnAStarFinished onFinished)
        {
            if (_hasRun && _running)
            {
                PrintError(onFinished);
                return false;
            }

            try
            {
                if (!ThreadPool.QueueUserWorkItem((e) => Run(goal, map)))
                    throw new Exception();

                _onFinished = onFinished;
                _running = true;
                _hasRun = true;
            }
            catch
            {
                PrintError(onFinished);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarGoal goal, AStarMap map)
        {
            // Test, waits for 2 seconds
            Thread.Sleep(2000);

            _running = false;
        }

        /// <summary>
        /// Prints an error message
        /// </summary>
        private void PrintError(OnAStarFinished onFinished)
        {
            if (onFinished == null)
            {
                Debugger.Log(LOG_TYPE.WARNING,
                    "Item could not be queued!\n");
            }
            else
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "Item could not be queued!\n{0}",
                    onFinished.Target.ToString());
            }
        }
    }

    public delegate void OnAStarFinished();
}