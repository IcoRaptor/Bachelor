using AI;
using AI.GOAP;
using UnityEngine;

public class TestAction : BaseAction
{
    public TestAction()
    {
        _preconditions = new STATE_SYMBOL[WorldState.NUM_SYMBOLS];
        _effects = new STATE_SYMBOL[WorldState.NUM_SYMBOLS];

        for (int i = 0; i < _effects.Length; i++)
        {
            _preconditions[i] = STATE_SYMBOL.UNSET;
            _effects[i] = STATE_SYMBOL.UNSET;
        }
    }

    public override void Update(AIModule module)
    {
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("TestAction activated!\n");
    }

    public override bool CheckContext()
    {
        return true;
    }

    public override bool IsValid()
    {
        return true;
    }

    public override BaseAction Copy()
    {
        var action = new TestAction();
        return action;
    }
}