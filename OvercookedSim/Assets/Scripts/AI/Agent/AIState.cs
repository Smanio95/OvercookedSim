using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    Idle,
    Working
}

public class AIState : MonoBehaviour
{
    public State currentState;
    public NavMeshAgent agent;
}
