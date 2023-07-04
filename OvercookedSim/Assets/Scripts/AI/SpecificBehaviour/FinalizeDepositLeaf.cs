using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizeDepositLeaf : Leaf
{
    private DishChooser dishChooser;
    private Resource[] resources;
    private int index;

    public FinalizeDepositLeaf(Resource[] _resources, DishChooser _dishChooser, int _index)
    {
        resources = _resources;
        dishChooser = _dishChooser;
        index = _index;
    }

    protected override BT_Status SpecificMethod()
    {
        Resource resource = ResourceFoodLeaf.GetResource(dishChooser, resources, index);
        resource.InsertIngredient();
        return BT_Status.Success;
    }
}
