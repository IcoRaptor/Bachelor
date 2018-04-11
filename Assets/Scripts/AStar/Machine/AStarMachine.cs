using Framework;
using Framework.Debugging;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace AStar
{
    public class AStarMachine : SingletonAsObject<AStarMachine>
    {
        #region Variables

        private readonly object _lock = new object();

        private List<AStarSolver> _solvers = new List<AStarSolver>();
        private Stack<AStarSolver> _solversToRemove = new Stack<AStarSolver>();
        private Dictionary<AStarSolver, AStarCallback> _callbacks =
            new Dictionary<AStarSolver, AStarCallback>();

        #endregion

        #region Properties

        public static AStarMachine Instance
        {
            get { return (AStarMachine)_Instance; }
        }

        #endregion

        private void Update()
        {
            lock (_lock)
            {
                while (_solversToRemove.Count > 0)
                {
                    var s = _solversToRemove.Pop();
                    _callbacks.Remove(s);
                    _solvers.Remove(s);
                }

                foreach (var s in _solvers)
                {
                    if (s.IsFinished())
                    {
                        _callbacks[s](s.Result);
                        _solversToRemove.Push(s);
                    }
                }
            }
        }

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public void RunAStar(AStarParams param)
        {
            try
            {
                if (param == null)
                    throw new ArgumentNullException("param");

                if (!ThreadPool.QueueUserWorkItem(e => Run(param)))
                    throw new Exception("Item could not be queued!");
            }
            catch (Exception e)
            {
                PrintError(param.Callback, e);
            }
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarParams param)
        {
            using (var solver = new AStarSolver(param))
            {
                lock (_lock)
                {
                    _solvers.Add(solver);
                    _callbacks.Add(solver, param.Callback);
                }

                solver.Solve();
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

        private void OnDestroy()
        {
            _solvers.Clear();
            _solversToRemove.Clear();
            _callbacks.Clear();
        }
    }
}