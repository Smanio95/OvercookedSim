using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float endPositionThreshold = 1.5f;
    public static float EndPositionThreshold;

    [Header("Refs")]
    [SerializeField] AIState myAgent;
    [SerializeField] DishChooser chooser;
    [SerializeField] Resource[] resources;
    [SerializeField] Deposit[] deposits;
    [SerializeField] WorkStation workStation;
    [SerializeField] Clock clock;
    [SerializeField] Transform bed;

    private BTRoot root = new();

    private void Awake()
    {
        EndPositionThreshold = endPositionThreshold;
    }

    private void Start() => GenerateTree();

    private void Update()
    {
        root.Process();
    }

    void GenerateTree()
    {
        // resting sequence
        Sequence restSequence = GenerateRestSeq();

        // cook sequence
        Leaf toWorkStation = new MovableLeaf(myAgent, workStation.transform);
        Sequence cook = GenerateCookSeq(toWorkStation);

        // waitOrWork
        Sequence waitOrWork = GenerateWaitOrWorkSeq(toWorkStation, cook);

        // restOrWork
        Selector restOrWork = new();

        restOrWork.AddChild(restSequence);
        restOrWork.AddChild(waitOrWork);

        root.AddChild(restOrWork);
    }

    Sequence GenerateCookSeq(Leaf toWorkStation)
    {
        Sequence cook = new();

        Sequence retrieveFood = RetrieveFoodSeq();
        Leaf actualCook = new WorkStationLeaf(chooser, workStation);

        cook.AddChild(retrieveFood);
        cook.AddChild(toWorkStation);
        cook.AddChild(actualCook);

        return cook;
    }

    Sequence RetrieveFoodSeq()
    {
        Sequence retrieveFood = new();
        for (int i = 0; i < resources.Length; i++)
        {
            Sequence resourceFoodSeq = GenerateResourceFoodSeq(i);

            Sequence getFromDeposit = GenerateDepositSeq(i);

            Selector executeRetrieval = new();
            executeRetrieval.AddChild(resourceFoodSeq);
            executeRetrieval.AddChild(getFromDeposit);

            retrieveFood.AddChild(executeRetrieval);
        }
        return retrieveFood;
    }

    Sequence GenerateRestSeq()
    {
        Sequence restSequence = new();
        
        Leaf goToSleep = new SleepLeaf(clock);
        Leaf toBed = new MovableLeaf(myAgent, bed);
        
        restSequence.AddChild(goToSleep);
        restSequence.AddChild(toBed);
        
        return restSequence;
    }

    Sequence GenerateResourceFoodSeq(int i)
    {
        Sequence resourceFoodSeq = new();

        Leaf retrieveResourceFood = new ResourceMovingLeaf(myAgent, resources, chooser, i);
        Leaf askFood = new ResourceFoodLeaf(resources, chooser, i);

        resourceFoodSeq.AddChild(retrieveResourceFood);
        resourceFoodSeq.AddChild(askFood);
        
        return resourceFoodSeq;
    }

    Sequence GenerateDepositSeq(int i)
    {
        Sequence getFromDeposit = new();

        Leaf retrieveDepositFood = new ResourceMovingLeaf(myAgent, deposits, chooser, i);
        Leaf toResourceFood = new ResourceMovingLeaf(myAgent, resources, chooser, i);
        Leaf finalizeDeposit = new FinalizeDepositLeaf(resources, chooser, i);
        Leaf reAskFood = new ResourceFoodLeaf(resources, chooser, i);

        getFromDeposit.AddChild(retrieveDepositFood);
        getFromDeposit.AddChild(toResourceFood);
        getFromDeposit.AddChild(finalizeDeposit);
        getFromDeposit.AddChild(reAskFood);

        return getFromDeposit;
    }

    Sequence GenerateWaitOrWorkSeq(Leaf toWorkStation, Sequence cook)
    {
        Sequence waitOrWork = new();

        Leaf idleLeaf = new IdleLeaf(chooser);
        Leaf startCooking = new WorkStationLeaf(null, workStation);

        waitOrWork.AddChild(toWorkStation);
        waitOrWork.AddChild(startCooking);
        waitOrWork.AddChild(idleLeaf);
        waitOrWork.AddChild(cook);

        return waitOrWork;
    }

}
