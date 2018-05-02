using Framework.Debugging;
using System.Collections.Generic;
using System.Linq;

namespace AI.GOAP
{
    /// <summary>
    /// Contains the goals and actions
    /// </summary>
    public static class GOAPContainer
    {
        #region Variables

        private static Dictionary<string, BaseAction> _actionCache;
        private static Dictionary<string, BaseGoal> _goalCache;
        private static Dictionary<string, GOAPAgent> _agentCache;

        private static List<BaseAction>[] _effectsTable;

        #endregion

        #region Properties

        public static bool Initialized { get; private set; }

        public static int ActionCount
        {
            get
            {
                return Initialized ?
                    _actionCache.Count :
                    0;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the GOAPContainer
        /// </summary>
        public static void Init()
        {
            if (Initialized)
                return;

            _actionCache = new Dictionary<string, BaseAction>();
            _goalCache = new Dictionary<string, BaseGoal>();
            _agentCache = new Dictionary<string, GOAPAgent>();

            _effectsTable = new List<BaseAction>[WorldState.SymbolCount];

            for (int i = 0; i < WorldState.SymbolCount; i++)
                _effectsTable[i] = new List<BaseAction>();

            Initialized = true;
        }

        /// <summary>
        /// Adds an action to the action cache and the effects table
        /// </summary>
        public static void AddAction(BaseAction action)
        {
            _actionCache.Add(action.ID, action);

            for (int i = 0; i < WorldState.SymbolCount; i++)
                if (action.Effects.Symbols[i] == STATE_SYMBOL.SATISFIED)
                    _effectsTable[i].Add(action);
        }

        /// <summary>
        /// Adds a goal to the goal cache
        /// </summary>
        public static void AddGoal(BaseGoal goal)
        {
            _goalCache.Add(goal.ID, goal);
        }

        /// <summary>
        /// Adss an agent to the agent cache
        /// </summary>
        public static void AddAgent(GOAPAgent agent)
        {
            _agentCache[agent.ID] = agent;
        }

        /// <summary>
        /// Returns the action with the given ID.
        ///  May return null
        /// </summary>
        public static BaseAction GetAction(string id)
        {
            // Check root
            if (id.Length == Strings.ROOT_NODE.Length)
                if (string.CompareOrdinal(id, Strings.ROOT_NODE) == 0)
                    return null;

            try
            {
                return _actionCache[id].Copy();
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "ActionID '{0}' doesn't exist!\n",
                    id);

                return null;
            }
        }

        /// <summary>
        /// Returns the goal with the given ID.
        ///  May return null
        /// </summary>
        public static BaseGoal GetGoal(string id)
        {
            try
            {
                return _goalCache[id].Copy();
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                       "GoalID '{0}' doesn't exist!\n",
                       id);

                return null;
            }
        }

        /// <summary>
        /// Returns the agent with the given ID.
        ///  May return null
        /// </summary>
        public static GOAPAgent GetAgent(string id)
        {
            try
            {
                return _agentCache[id].Copy();
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "AgentID '{0}' doesn't exist!\n",
                    id);

                return null;
            }
        }

        /// <summary>
        /// Chooses valid actions from the action array
        /// </summary>
        public static BaseAction[] ChooseActions(WorldState state, BaseAction[] actions)
        {
            List<BaseAction> allActions = new List<BaseAction>();

            // Get all fitting actions
            for (int i = 0; i < WorldState.SymbolCount; i++)
            {
                if (state.Symbols[i] != STATE_SYMBOL.UNSATISFIED)
                    continue;

                foreach (var action in _effectsTable[i])
                    allActions.Add(action.Copy());
            }

            // Remove unavailable actions
            var availableActions = allActions.Intersect(actions).ToList();
            availableActions.RemoveAll(action => !action.CheckContext());

            return availableActions.ToArray();
        }
    }
}