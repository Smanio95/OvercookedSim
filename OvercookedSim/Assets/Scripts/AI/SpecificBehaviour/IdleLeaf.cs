using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleLeaf : Leaf
{
    private DishChooser dishChooser;

    public IdleLeaf(DishChooser _dishChooser)
    {
        dishChooser = _dishChooser;
    }

    protected override BT_Status SpecificMethod()
    {
        if (!dishChooser.dishChosen) return BT_Status.Running;

        return BT_Status.Success;
    }
}
