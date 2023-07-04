using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dish
{
    public string name;
    public ScriptableIngredient[] ingredients;
    public float completionTime;
}

[CreateAssetMenu(fileName ="DishHolder", menuName ="ScriptableObjects/Dishes/Dish")]
public class ScriptableDish : ScriptableObject
{
    public Dish[] availableDishes;
    public Sprite nullIngredient;

    public Dish GetDish() => availableDishes[Random.Range(0, availableDishes.Length)];
}
