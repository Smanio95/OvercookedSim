using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableLeaf : Leaf
{
    protected AIState myAgent;

    protected Vector3 location;

    public MovableLeaf(AIState _myAgent)
    {
        myAgent = _myAgent;
    }

    public MovableLeaf(AIState _myAgent, Transform _toGo)
    {
        myAgent = _myAgent;
        location = _toGo.position;
    }

    protected override BT_Status SpecificMethod()
    {
        if(myAgent.currentState == State.Idle)
        {
            MoveAgent();
        }

        myAgent.currentState = State.Idle;

        if (Vector3.Distance(myAgent.transform.position, location) <= BTController.EndPositionThreshold)
        {
            return BT_Status.Success;
        }

        myAgent.currentState = State.Working;
        return BT_Status.Running;

    }

    protected virtual Vector3 GetLocation() => location;

    void MoveAgent()
    {
        location = GetLocation();
        myAgent.agent.SetDestination(location);
    }
}
