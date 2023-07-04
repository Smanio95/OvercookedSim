using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : VisualizableResource
{
    [SerializeField] TMP_Text availabilityHolder;
    [SerializeField] string splitStr = " / ";
    [Header("Stats")]
    [SerializeField] int maxResource = 5;
    [SerializeField] int actualResource;


    protected override void Start()
    {
        base.Start();
        actualResource = maxResource;
        ChangeAvailability();
    }

    void ChangeAvailability()
    {
        availabilityHolder.text = actualResource + splitStr + maxResource;
    }

    public IngredientType AskIngredient()
    {
        if (actualResource == 0) return IngredientType.None;

        actualResource--;
        ChangeAvailability();
        return ingredient.Type;
    }

    public void InsertIngredient()
    {
        actualResource = maxResource;
        ChangeAvailability();
    }
}
