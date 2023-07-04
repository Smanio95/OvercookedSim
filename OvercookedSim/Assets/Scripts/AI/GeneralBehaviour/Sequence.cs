using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public override BT_Status Process()
    {
        BT_Status childStatus = children[currentChild].Process();
        if (childStatus == BT_Status.Running) return BT_Status.Running;
        if (childStatus == BT_Status.Fail)
        {
            currentChild = 0;
            return childStatus;
        }

        currentChild++;
        if (currentChild >= children.Count)
        {
            currentChild = 0;
            return BT_Status.Success;
        }

        return BT_Status.Running;
    }
}
