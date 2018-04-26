namespace AI.GOAP
{
    public enum STATE_SYMBOL
    {
        UNSATISFIED = -1,
        UNSET,
        SATISFIED
    }

    public enum XML_TYPE
    {
        AGENT_SET,
        GOAL_SET,
        ACTION_SET
    }
}

namespace AStar
{
    public enum RETURN_CODE
    {
        DEFAULT,
        SUCCESS,
        ERROR,
        NO_PATH_FOUND
    }
}