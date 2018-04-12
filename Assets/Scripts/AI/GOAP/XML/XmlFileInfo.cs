public class XmlFileInfo
{
    #region Variables

    private const string _RELATIVE_BASE = @"Scripts/AI/GOAP/XML/Files";

    #endregion

    #region Properties

    public string FileName { get; private set; }

    public string RelativePath { get; private set; }

    #endregion

    #region Constructors

    public XmlFileInfo(string fileName)
    {
        FileName = fileName;
        RelativePath = _RELATIVE_BASE + FileName;
    }

    #endregion
}