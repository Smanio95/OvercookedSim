using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepLeaf : Leaf
{
    Clock clock;

    private bool hasBeenTriggered = true;

    public SleepLeaf(Clock _clock)
    {
        clock = _clock;
    }

    protected override BT_Status SpecificMethod()
    {
        if (clock.isOpen)
        {
            hasBeenTriggered = false;
            return BT_Status.Fail;
        }

        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            clock.GoToSleep();
        }

        return BT_Status.Success;
    }

}
