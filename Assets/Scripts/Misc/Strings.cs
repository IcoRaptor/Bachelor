public static class Strings
{
    // A*

    public const string ROOT_NODE = @"_R";

    // GOAP

    public const string GOAL_WORKING = "Working";
    public const string GOAL_SLEEPING = "Sleeping";
    public const string GOAL_EATING = "Eating";
    public const string GOAL_HAVING_FUN = "HavingFun";

    public const string ACTION_WORK = "Work";
    public const string ACTION_GO_TO_WORK = "GoToWork";
    public const string ACTION_GO_TO_HOME = "GoToHome";
    public const string ACTION_GO_TO_STORE = "GoToStore";
    public const string ACTION_SLEEP = "Sleep";
    public const string ACTION_NAP = "Nap";
    public const string ACTION_EAT = "Eat";
    public const string ACTION_BUY_FOOD = "BuyFood";
    public const string ACTION_GO_TO_BAR = "GoToBar";
    public const string ACTION_DRINK = "Drink";

    // WorldState symbols

    public const string STATE_WORKING = "working";
    public const string STATE_AT_HOME = "atHome";
    public const string STATE_RESTED = "rested";
    public const string STATE_FULL = "full";
    public const string STATE_HAPPY = "happy";
    public const string STATE_AT_WORK = "atWork";
    public const string STATE_AT_STORE = "atStore";
    public const string STATE_HAS_FOOD = "hasFood";
    public const string STATE_HAS_MONEY = "hasMoney";
    public const string STATE_AT_BAR = "atBar";
    public const string STATE_AT_SCHOOL = "atSchool";
    public const string STATE_AT_PLAYGROUND = "atPlayground";

    // Discontentment symbols

    public const string DISC_SLEEP = "sleep";
    public const string DISC_MONEY = "money";
    public const string DISC_HUNGER = "hunger";
    public const string DISC_FUN = "fun";

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