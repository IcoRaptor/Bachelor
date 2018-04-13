public class XmlFileInfo
{
    #region Properties

    /// <summary>
    /// Name of the file
    /// </summary>
    public string FileName { get; private set; }

    /// <summary>
    /// Path to the file relative to the Assets folder
    /// </summary>
    public string RelativePath { get; private set; }

    #endregion

    #region Constructors

    public XmlFileInfo(string fileName)
    {
        bool ending = fileName.Contains(".");

        if (ending)
            fileName = fileName.Split('.')[0];

#if UNITY_EDITOR
        FileName = fileName + Strings.XML;
        RelativePath = Strings.RELATIVE_BASE + FileName;
#else
        FileName = fileName;
#endif
    }

    #endregion
}