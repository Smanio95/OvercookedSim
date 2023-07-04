using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] Slider playerSpeed;

    public void ChangePlayerSpeed()
    {
        agent.speed = playerSpeed.value;
    }
}
