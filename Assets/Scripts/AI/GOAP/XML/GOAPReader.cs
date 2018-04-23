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
                return false;
            }

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

            foreach (XmlNode node in nodes)
            {
                id = node.Attributes[Strings.ATTR_ID].Value;
            }

            // Test
            var testGoal = new TestGoal()
            {
                ID = id
            };
            GOAPContainer.AddGoal(testGoal);

            return true;
        }

        private static bool ReadActionSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_ACTION);
            string id = string.Empty;

            foreach (XmlNode node in nodes)
                id = node.Attributes[Strings.ATTR_ID].Value;

            // Test
            var testAction = new TestAction()
            {
                ID = id
            };
            GOAPContainer.AddAction(testAction);

            return true;
        }

        private static bool ReadAgentSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_AGENT);
            string id = string.Empty;

            foreach (XmlNode node in nodes)
                id = node.Attributes[Strings.ATTR_ID].Value;

            // Test
            var agent = new Agent()
            {
                ID = id,
                Actions = new BaseAction[0],
                Goals = new BaseGoal[0]
            };
            GOAPContainer.AddAgent(agent);

            return true;
        }
    }
}