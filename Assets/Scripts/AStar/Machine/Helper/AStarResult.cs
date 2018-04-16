using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Contains the result of an AStar search
    /// </summary>
    public class AStarResult
    {
        public RETURN_CODE Code { get; set; }
        public LinkedList<AStarNode> Nodes { get; set; }

        public AStarResult()
        {
            Code = RETURN_CODE.DEFAULT;
        }
    }
}