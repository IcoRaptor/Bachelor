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
    public static class GOAPXmlReader
    {
        /// <summary>
        /// Reads the specified XML file and adds the content to the Container
        /// </summary>
        public static bool ReadFile(XmlFileInfo info)
        {
            if (info == null)
                return false;

#if UNITY_EDITOR
            return ReadWithDocument(info);
#else
            return ReadWithReader(info);
#endif
        }

        private static bool ReadWithDocument(XmlFileInfo info)
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

            /*
            var nodes = doc.SelectNodes("//x/path");

            foreach (XmlNode node in nodes)
            {
                var name = node.SelectSingleNode("name");
            }
            */

            return true;
        }

        private static bool ReadWithReader(XmlFileInfo info)
        {
            var file = Resources.Load(info.FileName) as TextAsset;

            if (file == null)
                return false;

            using (var stream = new MemoryStream(file.bytes, false))
                return ReadStream(stream);
        }

        private static bool ReadStream(MemoryStream stream)
        {
            using (var reader = XmlReader.Create(stream))
            {
                while (reader.Read())
                {
                    if (!reader.IsStartElement())
                        continue;

                    switch (reader.Name)
                    {
                        default:
                            break;
                    }
                }
            }

            return true;
        }
    }
}