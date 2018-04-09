using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Contains the result of an AStar search
    /// </summary>
    public struct AStarResult
    {
        public RETURN_CODE Code { get; set; }
        public LinkedList<AStarNode> Nodes { get; set; }

        public AStarResult(RETURN_CODE code, LinkedList<AStarNode> nodes)
        {
            Code = code;
            Nodes = nodes;
        }
    }
}