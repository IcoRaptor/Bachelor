public class XmlFileInfo
{
    #region Variables

    private const string _XML = @".xml";
    private const string _RELATIVE_BASE = @"Scripts/AI/GOAP/XML/Resources/";

    #endregion

    #region Properties

    public string FileName { get; private set; }

    public string RelativePath { get; private set; }

    #endregion

    #region Constructors

    public XmlFileInfo(string fileName)
    {
        string end = fileName.EndsWith(_XML) ?
            string.Empty :
            _XML;

        FileName = fileName + end;
        RelativePath = _RELATIVE_BASE + FileName;
    }

    #endregion
}