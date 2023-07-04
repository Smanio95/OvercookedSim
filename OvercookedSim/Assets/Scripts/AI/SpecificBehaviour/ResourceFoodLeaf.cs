using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFoodLeaf : Leaf
{
    Resource[] resources;
    DishChooser dishChooser;
    int index;

    public ResourceFoodLeaf(Resource[] _resources, DishChooser _dishChooser, int _index)
    {
        resources = _resources;
        dishChooser = _dishChooser;
        index = _index;
    }

    protected override BT_Status SpecificMethod()
    {
        Resource resource = GetResource(dishChooser, resources, index);
        
        if (resource.AskIngredient() == IngredientType.None)
        {
            return BT_Status.Fail;
        }
        return BT_Status.Success;
    }

    public static Resource GetResource(DishChooser dishChooser, Resource[] resources, int index)
    {
        IngredientType type = dishChooser.ChosenDish.ingredients[index].Type;

        foreach (Resource resource in resources)
        {
            if (resource.Type == type)
            {
                return resource;
            }
        }
        return null;
    }
}
