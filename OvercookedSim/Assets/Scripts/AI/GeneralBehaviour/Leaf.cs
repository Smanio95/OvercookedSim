using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Leaf : Node
{
    public Leaf() { }

    protected abstract BT_Status SpecificMethod();

    public override BT_Status Process() => SpecificMethod();
}
