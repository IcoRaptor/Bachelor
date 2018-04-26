using AI;
using AI.GOAP;
using UnityEngine;

public class TestAction : BaseAction
{
    public TestAction()
    {
        Preconditions = new WorldState();
        Effects = new WorldState();

        for (int i = 0; i < WorldState.NUM_SYMBOLS; i++)
        {
            Preconditions.Symbols[i] = STATE_SYMBOL.UNSET;
            Effects.Symbols[i] = STATE_SYMBOL.SATISFIED;
        }
    }

    public override void Update(AIModule module)
    {
    }

    public override void Activate()
    {
        base.Activate();
        Debug.LogFormat(
            "{0}\nCost: {1}, Time: {2}",
            Dialog, Cost, TimeInMinutes);
    }

    public override bool CheckContext()
    {
        return true;
    }

    public override bool Validate()
    {
        return true;
    }

    public override BaseAction Copy()
    {
        return new TestAction()
        {
            ID = ID,
            Dialog = Dialog,
            Cost = Cost,
            TimeInMinutes = TimeInMinutes,
            Effects = Effects.Copy(),
            Preconditions = Preconditions.Copy()
        };
    }
}