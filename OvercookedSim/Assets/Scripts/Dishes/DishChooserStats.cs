using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DishChooserStats", menuName = "ScriptableObjects/Dishes/DishChooserStatInfo")]
public class DishChooserStats : ScriptableObject
{
    public string currentDishBaseString = "Current dish: ";
    public float choosingTimer = 1;
    public string choosingDishString = "Choosing current dish...";
    public string completedDishString = "Dish completed!";
    public float completedDishRestTimer = 2;
    public int maxDishIngredients = 3;
}
