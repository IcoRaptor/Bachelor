using Framework.Debugging;
using System.Collections.Generic;

namespace AI.GOAP
{
    /// <summary>
    /// Contains the goals and actions
    /// </summary>
    public static class GOAPContainer
    {
        #region Variables

        private static Dictionary<string, BaseAction> _actionCache =
            new Dictionary<string, BaseAction>();
        private static Dictionary<string, BaseGoal> _goalCache =
            new Dictionary<string, BaseGoal>();

        private static Dictionary<string, BaseAction[]> _agentActions =
            new Dictionary<string, BaseAction[]>();
        private static Dictionary<string, BaseGoal[]> _agentGoals =
            new Dictionary<string, BaseGoal[]>();

        // TODO Neighbours

        #endregion

        /// <summary>
        /// Returns the number of cached actions
        /// </summary>
        public static int ActionCount()
        {
            return _actionCache.Count;
        }

        /// <summary>
        /// Adds an action to the action cache
        /// </summary>
        public static void AddAction(BaseAction action)
        {
            if (!_actionCache.ContainsKey(action.ID))
                _actionCache.Add(action.ID, action);
        }

        /// <summary>
        /// Returns the action with the given ID.
        ///  May return null
        /// </summary>
        public static BaseAction GetAction(string id)
        {
            try
            {
                return _actionCache[id];
            }
            catch
            {
                if (string.CompareOrdinal(id, Strings.ROOT_NODE) != 0)
                {
                    Debugger.LogFormat(LOG_TYPE.WARNING,
                        "GetAction: ID '{0}' doesn't exist!\n",
                        id);
                }

                return null;
            }
        }

        /// <summary>
        /// Adds a goal to the goal cache
        /// </summary>
        public static void AddGoal(BaseGoal goal)
        {
            if (!_goalCache.ContainsKey(goal.ID))
                _goalCache.Add(goal.ID, goal);
        }

        /// <summary>
        /// Returns the goal with the given ID.
        ///  May return null
        /// </summary>
        public static BaseGoal GetGoal(string id)
        {
            try
            {
                return _goalCache[id];
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                       "GetGoal: ID '{0}' doesn't exist!\n",
                       id);

                return null;
            }
        }

        /// <summary>
        /// Adss an agent to the agent cache
        /// </summary>
        public static void AddAgent(Agent agent)
        {
            _agentGoals[agent.ID] = agent.Goals;
            _agentActions[agent.ID] = agent.Actions;
        }

        /// <summary>
        /// Fills in the goals and actions of the agent with the given ID
        /// </summary>
        public static void GetAgent(string id,
            out BaseGoal[] goals,
            out BaseAction[] actions)
        {
            try
            {
                goals = _agentGoals[id];
                actions = _agentActions[id];
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "GetAgent: ID '{0}' doesn't exist!\n",
                    id);

                goals = new BaseGoal[0];
                actions = new BaseAction[0];
            }
        }
    }
}