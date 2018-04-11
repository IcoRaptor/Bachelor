using System.Xml;

namespace AI.GOAP
{
    /// <summary>
    /// Reads the actions and goals from XML
    /// </summary>
    public static class GOAPXmlReader
    {
        public static bool ReadFile(string fileName)
        {
            using (var reader = XmlReader.Create(fileName))
            {
            }

            return false;
        }
    }
}