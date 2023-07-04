using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualizableResource : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] protected ScriptableIngredient ingredient;
    [SerializeField] TMP_Text nameHolder;

    public IngredientType Type { get => ingredient.Type; }

    protected virtual void Start()
    {
        nameHolder.text = ingredient.IngredientName;
    }
}
