public static class Strings
{
    // A*

    public const string ROOT_NODE = @"r";

    // XmlFileInfo

    public const string XML = @".xml";
    public const string RELATIVE_BASE = @"Scripts/AI/GOAP/XML/Resources/";

    public const string GOAL_SET = @"GoalSet";
    public const string ACTION_SET = @"ActionSet";
    public const string AGENT_SET = @"AgentSet";

    // Xml

    public const string GOAL = @"goal";
    public const string ACTION = @"action";
    public const string AGENT = @"agent";

    public const string ATTR_ID = @"id";

    // XPath

    public const string XPATH_GOAL = @"//" + GOAL_SET + @"/" + GOAL;
    public const string XPATH_ACTION = @"//" + ACTION_SET + @"/" + ACTION;
    public const string XPATH_AGENT = @"//" + AGENT_SET + @"/" + AGENT;
}