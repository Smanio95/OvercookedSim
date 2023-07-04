using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    None,
    Chicken,
    Egg,
    Tomato
}

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Dishes/Ingredient")]
public class ScriptableIngredient : ScriptableObject
{
    [SerializeField] string ingredientName;
    [SerializeField] IngredientType type;
    [SerializeField] Sprite sprite;

    public string IngredientName { get => ingredientName; }
    public IngredientType Type { get => type; }
    public Sprite Sprite { get => sprite; }
}
