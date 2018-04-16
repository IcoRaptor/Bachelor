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

            if (!ReadFile(goalSet, XML_TYPE.GOAL_SET))
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
            {
                Debugger.LogFormat(LOG_TYPE.ERROR,
                    "File '{0}' not found!\n",
                    info.FileName);

                return false;
            }

            var doc = new XmlDocument();

            try
            {
                using (var stream = new MemoryStream(file.bytes, false))
                    doc.Load(stream);
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

        private static bool ReadDocument(XmlDocument doc, XML_TYPE type)
        {
            switch (type)
            {
                case XML_TYPE.AGENT:
                    return ReadAgent(doc);

                case XML_TYPE.ACTION_SET:
                    return ReadActionSet(doc);

                case XML_TYPE.GOAL_SET:
                    return ReadGoalSet(doc);
            }

            return false;
        }

        private static bool ReadAgent(XmlDocument doc)
        {
            return true;
        }

        private static bool ReadActionSet(XmlDocument doc)
        {
            return true;
        }

        private static bool ReadGoalSet(XmlDocument doc)
        {
            var nodes = doc.SelectNodes(Strings.XPATH_GOAL);

            foreach (XmlNode node in nodes)
            {
                Debug.Log(node.Attributes[Strings.ATTR_ID].Value + "\n");
            }

            return true;
        }
    }
}