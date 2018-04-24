using Framework.Debugging;
using System;
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

        /// <summary>
        /// Reads the specified XML file
        /// </summary>
        public static bool ReadFile(XmlFileInfo info, XML_TYPE type)
        {
            if (info == null)
                return false;

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
                    "Couldn't load the XmlDocument!\n{0}",
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

                // TODO: target WorldState

                BaseGoal goal = null;

                switch (id)
                {
                    case Strings.GOAL_TEST:
                        goal = new TestGoal()
                        {
                            ID = id
                        };
                        break;

                    default:
                        Debugger.LogFormat(LOG_TYPE.WARNING,
                            "GoalID '{0}' is not defined.\n",
                            id);
                        break;
                }

                if (goal != null)
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

                // TODO: conditions and effects

                BaseAction action = null;

                switch (id)
                {
                    case Strings.ACTION_TEST:
                        action = new TestAction()
                        {
                            ID = id,
                            Dialog = dialog,
                            Cost = cost,
                            TimeInMinutes = time
                        };
                        break;

                    default:
                        Debugger.LogFormat(LOG_TYPE.WARNING,
                            "ActionID '{0}' is not defined.\n",
                            id);
                        break;
                }

                if (action != null)
                    GOAPContainer.AddAction(action);
            }

            return true;
        }

        private static bool ReadAgentSet(XmlDocument doc)
        {
            var agents = doc.SelectNodes(Strings.XPATH_AGENT);

            foreach (XmlNode agentNode in agents)
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

        private static void DestroyFile(TextAsset file)
        {
            UnityEngine.Object.Destroy(file);
            Resources.UnloadUnusedAssets();
        }
    }
}