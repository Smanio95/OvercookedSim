using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationLeaf : Leaf
{
    private DishChooser dishChooser;
    private WorkStation workStation;
    private float elapsed = 0;

    public delegate void StartWork();
    public static StartWork OnStartWork;

    public delegate void DishCompleted();
    public static DishCompleted OnDishCompleted;

    public WorkStationLeaf(DishChooser _dishChooser, WorkStation _workStation)
    {
        dishChooser = _dishChooser;
        workStation = _workStation;
    }

    protected override BT_Status SpecificMethod()
    {
        if(dishChooser == null)
        {
            OnStartWork?.Invoke();
            return BT_Status.Success;
        }

        workStation.UpdateText(true);

        elapsed += Time.deltaTime;

        if(elapsed >= dishChooser.ChosenDish.completionTime)
        {
            elapsed = 0;
            workStation.UpdateText(false);
            OnDishCompleted?.Invoke();
            return BT_Status.Success;
        }
        return BT_Status.Running;
    }

}
