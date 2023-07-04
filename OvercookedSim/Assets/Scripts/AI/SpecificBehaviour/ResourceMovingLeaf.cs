using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMovingLeaf : MovableLeaf
{
    protected VisualizableResource[] toGo;
    protected DishChooser dishChooser;
    protected int index;

    public ResourceMovingLeaf(AIState _myAgent, VisualizableResource[] _toGo, DishChooser _dishChooser, int _index) : base(_myAgent)
    {
        toGo = _toGo;
        dishChooser = _dishChooser;
        index = _index;
    }

    protected override Vector3 GetLocation()
    {
        IngredientType type = dishChooser.ChosenDish.ingredients[index].Type;

        foreach (VisualizableResource vr in toGo)
        {
            if (vr.Type == type)
            {
                return vr.transform.position;
            }
        }
        return Vector3.zero;
    }
}
