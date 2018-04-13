﻿namespace AI.GOAP
{
    /// <summary>
    /// Represents a world state symbol
    /// </summary>
    public enum STATE_SYMBOL
    {
        UNSET,
        SATISFIED,
        UNSATISFIED
    }

    public enum XML_TYPE
    {
        AGENT,
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
    }
}