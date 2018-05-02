using Framework.Debugging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace AI.GOAP
{
    /// <summary>
    /// Reads the actions and goals from XML
    /// </summary>
    public static class GOAPReader
    {
        /// <summary>
        /// Reads all XML files
        /// </summary>
        public static bool ReadAll()
        {
            if (!GOAPContainer.Initialized)
                GOAPContainer.Init();

            var goalSet = new XmlFileInfo(Strings.GOAL_SET);
            var actionSet = new XmlFileInfo(Strings.ACTION_SET);
            var agentSet = new XmlFileInfo(Strings.AGENT_SET);

            if (!ReadFile(goalSet, XML_TYPE.GOAL_SET))
                return false;

            if (!ReadFile(actionSet, XML_TYPE.ACTION_SET))
                return false;

            if (!ReadFile(agentSet, XML_TYPE.AGENT_SET))
                return false;

            return true;
        }

        private static bool ReadFile(XmlFileInfo info, XML_TYPE type)
        {
#if UNITY_EDITOR
            return ReadEditor(info, type);
#else
            return ReadRelease(info, type);
#endif
        }

        private static bool ReadEditor(XmlFileInfo info, XML_TYPE type)
        {
            var doc = new XmlDocument();

            try
            {
                string path = Path.Combine(
                    Application.dataPath,
                    info.RelativePath);

                doc.Load(path);
            }
            catch (Exception e)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                    "LoadDocument failed!\n{0}",
                    e.ToString());
                return false;
            }

            return ReadDocument(doc, type);
        }

        private static bool ReadRelease(XmlFileInfo info, XML_TYPE type)
        {
            var file = Resources.Load(info.FileName) as TextAsset;

            if (file == null)
                return false;

            var doc = new XmlDocument();

            try
            {
                using (var stream = new MemoryStream(file.bytes, false))
                    doc.Load(stream);
            }
            catch
            {
                DestroyFile(file);
                return false;
            }

            DestroyFile(file);

            return ReadDocument(doc, type);
        }

        private static bool ReadDocument(XmlDocument doc, XML_TYPE type)
        {
            try
            {
                switch (type)
                {
                    case XML_TYPE.GOAL_SET:
                        return ReadGoalSet(doc);

                    case XML_TYPE.ACTION_SET:
                        return ReadActionSet(doc);

                    case XML_TYPE.AGENT_SET:
                        return ReadAgentSet(doc);
                }
            }
            catch (Exception e)
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                    "ReadDocument failed!\n{0}",
                    e.ToString());
            }

            return false;
        }

        private static bool ReadGoalSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_GOAL);

            foreach (XmlNode goalNode in nodes)
            {
                string id = goalNode.Attributes[Strings.ATTR_ID].Value;

                var relevanceList = goalNode.ChildNodes.Item(0).ChildNodes;
                var targetList = goalNode.ChildNodes.Item(1).ChildNodes;

                var indices = ReadRelevanceIndices(relevanceList);
                var target = ReadWorldState(targetList);

                BaseGoal goal = null;

                switch (id)
                {
                    case Strings.GOAL_WORKING:
                        goal = new WorkingGoal();
                        break;

                    case Strings.GOAL_SLEEPING:
                        goal = new SleepingGoal();
                        break;

                    case Strings.GOAL_EATING:
                        goal = new EatingGoal();
                        break;

                    case Strings.GOAL_HAVING_FUN:
                        goal = new HavingFunGoal();
                        break;

                    default:
                        Debugger.LogFormat(LOG_TYPE.WARNING,
                            "GoalID '{0}' is not defined.\n",
                            id);
                        continue;
                }

                goal.ID = id;
                goal.Target = target;
                goal.RelevanceIndices = indices;

                GOAPContainer.AddGoal(goal);
            }

            return true;
        }

        private static bool ReadActionSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_ACTION);

            foreach (XmlNode actionNode in nodes)
            {
                string id = actionNode.Attributes[Strings.ATTR_ID].Value;

                string dialog = actionNode.ChildNodes.Item(0).InnerText;
                int cost = int.Parse(actionNode.ChildNodes.Item(1).InnerText);
                int time = int.Parse(actionNode.ChildNodes.Item(2).InnerText);

                var conditionList = actionNode.ChildNodes.Item(3).ChildNodes;
                var effectList = actionNode.ChildNodes.Item(4).ChildNodes;

                var conditions = ReadWorldState(conditionList);
                var effects = ReadWorldState(effectList);

                BaseAction action = null;

                switch (id)
                {
                    case Strings.ACTION_WORK:
                        action = new WorkAction();
                        break;

                    case Strings.ACTION_GO_TO_WORK:
                        action = new GoToWorkAction();
                        break;

                    case Strings.ACTION_GO_TO_HOME:
                        action = new GoToHomeAction();
                        break;

                    case Strings.ACTION_SLEEP:
                        action = new SleepAction();
                        break;

                    case Strings.ACTION_NAP:
                        action = new NapAction();
                        break;

                    case Strings.ACTION_EAT:
                        action = new EatAction();
                        break;

                    case Strings.ACTION_GO_TO_STORE:
                        action = new GoToStoreAction();
                        break;

                    case Strings.ACTION_BUY_FOOD:
                        action = new BuyFoodAction();
                        break;

                    case Strings.ACTION_GO_TO_BAR:
                        action = new GoToBarAction();
                        break;

                    case Strings.ACTION_DRINK:
                        action = new DrinkAction();
                        break;

                    case Strings.ACTION_GO_TO_PLAYGROUND:
                        action = new GoToPlaygroundAction();
                        break;

                    case Strings.ACTION_GO_TO_SCHOOL:
                        action = new GoToSchoolAction();
                        break;

                    case Strings.ACTION_LEARN:
                        action = new LearnAction();
                        break;

                    case Strings.ACTION_PLAY:
                        action = new PlayAction();
                        break;

                    case Strings.ACTION_PATROL:
                        action = new PatrolAction();
                        break;

                    default:
                        Debugger.LogFormat(LOG_TYPE.WARNING,
                            "ActionID '{0}' is not defined.\n",
                            id);
                        continue;
                }

                action.ID = id;
                action.Dialog = dialog;
                action.Cost = cost;
                action.TimeInMinutes = time;
                action.Preconditions = conditions;
                action.Effects = effects;

                GOAPContainer.AddAction(action);
            }

            return true;
        }

        private static bool ReadAgentSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_AGENT);

            foreach (XmlNode agentNode in nodes)
            {
                string id = agentNode.Attributes[Strings.ATTR_ID].Value;

                var actionList = agentNode.ChildNodes.Item(0).ChildNodes;
                var goalList = agentNode.ChildNodes.Item(1).ChildNodes;

                var actions = new BaseAction[actionList.Count];
                var goals = new BaseGoal[goalList.Count];

                for (int i = 0; i < actionList.Count; i++)
                {
                    string aID = actionList.Item(i).InnerText;
                    actions[i] = GOAPContainer.GetAction(aID);
                }

                for (int i = 0; i < goalList.Count; i++)
                {
                    string gID = goalList.Item(i).InnerText;
                    goals[i] = GOAPContainer.GetGoal(gID);
                }

                var agent = new GOAPAgent()
                {
                    ID = id,
                    Actions = actions,
                    Goals = goals
                };

                GOAPContainer.AddAgent(agent);
            }

            return true;
        }

        private static int[] ReadRelevanceIndices(XmlNodeList list)
        {
            List<int> indices = new List<int>();

            foreach (XmlNode item in list)
            {
                string id = item.InnerText;
                int index = GOAPResolver.GetIndexFromSymbol(id, RESOLVE.DISCONTENTMENT);

                if (index != -1)
                    indices.Add(index);
            }

            return indices.ToArray();
        }

        private static WorldState ReadWorldState(XmlNodeList list)
        {
            var state = new WorldState();

            foreach (XmlNode item in list)
            {
                string id = item.InnerText;
                int index = GOAPResolver.GetIndexFromSymbol(id, RESOLVE.WORLD_STATE);

                if (index == -1)
                    continue;

                state[index] = STATE_SYMBOL.SATISFIED;
            }

            return state;
        }

        private static void DestroyFile(TextAsset file)
        {
            UnityEngine.Object.Destroy(file);
            Resources.UnloadUnusedAssets();
        }
    }
}