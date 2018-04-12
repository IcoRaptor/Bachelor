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

        private const float _UPDATES_PER_SECOND = 5f;

        private readonly object _lock = new object();

        private List<AStarSolver> _solvers = new List<AStarSolver>();
        private Stack<AStarSolver> _solversToRemove = new Stack<AStarSolver>();
        private Dictionary<AStarSolver, AStarCallback> _callbacks =
            new Dictionary<AStarSolver, AStarCallback>();

        private float _delta;

        #endregion

        #region Properties

        public static AStarMachine Instance
        {
            get { return (AStarMachine)_Instance; }
        }

        #endregion

        private void Update()
        {
            _delta += Time.unscaledDeltaTime * _UPDATES_PER_SECOND;

            if (_delta < 1.0f)
                return;

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

            _delta = 0;
        }

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public void RunAStar(AStarParams asp)
        {
            try
            {
                if (asp == null)
                    throw new ArgumentNullException("param");

                if (!ThreadPool.QueueUserWorkItem(e => Run(asp)))
                    throw new Exception("Item could not be queued!");
            }
            catch (Exception e)
            {
                PrintError(asp.Callback, e);
            }
        }

        /// <summary>
        /// Runs A* on the ThreadPool
        /// </summary>
        private void Run(AStarParams asp)
        {
            using (var solver = new AStarSolver(asp))
            {
                lock (_lock)
                {
                    _solvers.Add(solver);
                    _callbacks.Add(solver, asp.Callback);
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