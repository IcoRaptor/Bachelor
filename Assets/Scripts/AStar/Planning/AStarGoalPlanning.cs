﻿using AI.GOAP;

namespace AStar
{
    public class AStarGoalPlanning : AStarGoal
    {
        #region Properties

        public WorldState Target { get; private set; }
        public WorldState Current { get; private set; }
        public WorldState Temp { get; private set; }

        public BaseAction[] Actions { get; private set; }

        #endregion

        #region Constructors

        public AStarGoalPlanning(BaseGoal goal)
        {
            Target = goal.Target.Copy();
            Current = goal.Current.Copy();
            Actions = goal.Actions;

            Temp = new WorldState();

            for (int i = 0; i < WorldState.SymbolCount; i++)
            {
                if (GOAPResolver.GetMovementActionFromIndex(i))
                    Current.Symbols[i] = STATE_SYMBOL.UNSATISFIED;

                if (Target.Symbols[i] != STATE_SYMBOL.SATISFIED)
                    continue;

                Temp.Symbols[i] = Current.Symbols[i];
            }
        }

        #endregion

        public override float DistanceToTarget(AStarNode node)
        {
            var pNode = (AStarNodePlanning)node;
            var action = GOAPContainer.GetAction(pNode.ID);

            if (action == null)
                return 0;

            var tempState = action.ApplyEffects(pNode.Current);
            float distance = tempState.GetSymbolDifference(Target);

            return distance * distance * 10;
        }

        public override bool CheckNode(AStarNode node)
        {
            var pNode = (AStarNodePlanning)node;
            var action = GOAPContainer.GetAction(pNode.ID);

            if (action == null)
                return false;

            Temp = pNode.Current.Copy();

            UpdateTemp(Temp);
            Temp = action.ApplyEffects(Temp);
            Target = action.ApplyPreconditions(Target);

            pNode.Current = Temp.Copy();

            return pNode.Current.Satisfies(Target);
        }

        private void UpdateTemp(WorldState temp)
        {
            for (int i = 0; i < WorldState.SymbolCount; i++)
                if (Target.Symbols[i] == STATE_SYMBOL.SATISFIED)
                    if (Temp.Symbols[i] != STATE_SYMBOL.SATISFIED)
                        Temp.Symbols[i] = Current.Symbols[i];
        }
    }
}