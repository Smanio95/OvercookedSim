using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationMovingLeaf : MovableLeaf
{
    WorkStation workStation;

    public WorkStationMovingLeaf(AIState _myAgent, WorkStation _workStation) : base(_myAgent, _workStation.transform)
    {
        workStation = _workStation;
    }

    protected override Vector3 GetLocation() => workStation.transform.position;
}
