using Framework.Debugging;
using System.Threading;

namespace AStar
{
    public class AStarMachine
    {
        #region Variables

        private volatile bool _finished = false;

        #endregion

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public void RunAStar(AStarGoal goal, AStarMap map)
        {
            try
            {
                ThreadPool.QueueUserWorkItem((e) => Run(goal, map));
            }
            catch
            {
                Debugger.Log(LOG_TYPE.ERROR,
                    "Item could not be queued!\n");
            }
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarGoal goal, AStarMap map)
        {
            AStarStorage storage = new AStarStorage();

            Thread.Sleep(2000);

            _finished = true;
        }

        public void CheckFinished(OnFinished onFinished)
        {
            if (!_finished)
                return;

            _finished = false;
            onFinished();
        }
    }

    public delegate void OnFinished();
}