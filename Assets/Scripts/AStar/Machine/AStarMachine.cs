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

        [SerializeField]
        [Range(1, 10)]
        private int _updatePerSecond = 5;

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

        /// <summary>
        /// Queues an execution of A*
        /// </summary>
        public void RunAStar(AStarParams asp)
        {
            try
            {
                bool aspNull = asp == null ||
                    asp.Callback == null ||
                    asp.Goal == null ||
                    asp.Map == null;

                if (aspNull)
                    throw new ArgumentNullException("AStarParams");

                if (!ThreadPool.QueueUserWorkItem(e => Run(asp)))
                    throw new Exception("Item couldn't be queued!");
            }
            catch (Exception e)
            {
                PrintError(asp, e);
            }
        }

        private void Update()
        {
            _delta += Time.unscaledDeltaTime * _updatePerSecond;

            if (_delta < 1.0f)
                return;

            lock (_lock)
            {
                RemoveSolvers();
                UpdateSolvers();
            }

            _delta = 0;
        }

        private void RemoveSolvers()
        {
            while (_solversToRemove.Count > 0)
            {
                var s = _solversToRemove.Pop();
                _callbacks.Remove(s);
                _solvers.Remove(s);
            }
        }

        private void UpdateSolvers()
        {
            foreach (var s in _solvers)
            {
                if (!s.Finished())
                    continue;

                _callbacks[s](s.Result);
                _solversToRemove.Push(s);
            }
        }

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

        private void PrintError(AStarParams asp, Exception e)
        {
            bool callbackNull = asp == null || asp.Callback == null;

            string cb = callbackNull ?
                "No callback defined" :
                asp.Callback.Method.Name;

            string go = callbackNull ?
                string.Empty :
                ((MonoBehaviour)asp.Callback.Target).transform.parent.name;

            if (callbackNull)
            {
                Debugger.Log(LOG_TYPE.WARNING, e.Message + "\n" + cb);
                return;
            }

            Debugger.LogFormat(LOG_TYPE.WARNING,
                "{0}\nCallback: {1}, GO: {2}",
                e.Message, cb, go);
        }

        private void OnDestroy()
        {
            _solvers.Clear();
            _solversToRemove.Clear();
            _callbacks.Clear();
        }
    }
}