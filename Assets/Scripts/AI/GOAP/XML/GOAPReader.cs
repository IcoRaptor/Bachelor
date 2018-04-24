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
                    "{0}\n({1})",
                    e.Message, e.TargetSite);

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
            switch (type)
            {
                case XML_TYPE.GOAL_SET:
                    return ReadGoalSet(doc);

                case XML_TYPE.ACTION_SET:
                    return ReadActionSet(doc);

                case XML_TYPE.AGENT_SET:
                    return ReadAgentSet(doc);
            }

            return false;
        }

        private static bool ReadGoalSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_GOAL);
            string id = string.Empty;

            foreach (XmlNode goalNode in nodes)
            {
                id = goalNode.Attributes[Strings.ATTR_ID].Value;

                switch (id)
                {
                    case Strings.GOAL_TEST:
                        var testGoal = new TestGoal()
                        {
                            ID = id
                        };
                        GOAPContainer.AddGoal(testGoal);
                        break;
                }
            }

            return true;
        }

        private static bool ReadActionSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_ACTION);
            string id = string.Empty;

            foreach (XmlNode actionNode in nodes)
            {
                id = actionNode.Attributes[Strings.ATTR_ID].Value;

                switch (id)
                {
                    case Strings.ACTION_TEST:
                        var testAction = new TestAction()
                        {
                            ID = id
                        };
                        GOAPContainer.AddAction(testAction);
                        break;
                }
            }

            return true;
        }

        private static bool ReadAgentSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_AGENT);
            string id = string.Empty;

            foreach (XmlNode agentNode in nodes)
            {
                id = agentNode.Attributes[Strings.ATTR_ID].Value;

                switch (id)
                {
                    case Strings.AGENT_TEST:
                        var action = GOAPContainer.GetAction(Strings.ACTION_TEST);
                        var goal = GOAPContainer.GetGoal(Strings.GOAL_TEST);

                        BaseAction[] actions = { action };
                        BaseGoal[] goals = { goal };

                        var agent = new Agent()
                        {
                            ID = id,
                            Actions = actions,
                            Goals = goals
                        };
                        GOAPContainer.AddAgent(agent);
                        break;
                }
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