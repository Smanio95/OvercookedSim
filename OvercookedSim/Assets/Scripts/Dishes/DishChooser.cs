using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct IngredientsRetrieval
{
    public Image[] ingredientsRetrieved;
    public Sprite retrievedSprite;
    public Sprite notRetrievedSprite;
}

public class DishChooser : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] TMP_Text stringHolder;
    [SerializeField] TMP_Text completedDishesTxt;
    [SerializeField] Image[] ingredientsVisualization;
    [SerializeField] IngredientsRetrieval retrieval;
    [SerializeField] ScriptableDish dishes;
    [Header("Stats")]
    [SerializeField] DishChooserStats statInfo;

    [HideInInspector] public bool dishChosen = false;

    private int completedDishes = 0;
    private int retrievedCounter = 0;

    private Dish chosenDish;
    public Dish ChosenDish { get => chosenDish; }

    private void Awake()
    {
        WorkStationLeaf.OnStartWork += StartWork;
        WorkStationLeaf.OnDishCompleted += RestartDish;
        Resource.OnIngredientRetrieved += IngredientRetrieved;
        Clock.OnClosing += ResetDish;
    }

    void Start()
    {
        if (dishes == null)
        {
            Debug.Log("No dishes...");
            return;
        }

        //ResetDish();
    }

    private void OnDestroy()
    {
        WorkStationLeaf.OnStartWork -= StartWork;
        WorkStationLeaf.OnDishCompleted -= RestartDish;
        Resource.OnIngredientRetrieved -= IngredientRetrieved;
        Clock.OnClosing += ResetDish;
    }

    void IngredientRetrieved()
    {
        retrieval.ingredientsRetrieved[retrievedCounter].sprite = retrieval.retrievedSprite;
        retrievedCounter++;
    }

    void ResetDish()
    {
        StopAllCoroutines();
        stringHolder.text = "- - -";

        ResetDishUI();
    }

    Dish ChooseDish()
    {
        stringHolder.text = statInfo.choosingDishString;

        ResetDishUI();

        return dishes.GetDish();
    }

    void ResetDishUI()
    {
        for (int i = 0; i < ingredientsVisualization.Length; i++)
        {
            ingredientsVisualization[i].sprite = dishes.nullIngredient;
            retrieval.ingredientsRetrieved[i].sprite = retrieval.notRetrievedSprite;
        }
        retrievedCounter = 0;
    }

    void VisualizeDish(Dish chosenDish)
    {
        stringHolder.text = statInfo.currentDishBaseString + chosenDish.name;

        for(int i = 0; i < ingredientsVisualization.Length; i++)
        {
            ingredientsVisualization[i].sprite = chosenDish.ingredients[i].Sprite;
        }

    }

    IEnumerator DishCoroutine()
    {
        chosenDish = ChooseDish();
        yield return new WaitForSeconds(statInfo.choosingTimer);
        dishChosen = true;
        VisualizeDish(chosenDish);
    }

    private void StartWork() => StartCoroutine(DishCoroutine());

    private void RestartDish()
    {
        completedDishes++;

        completedDishesTxt.text = completedDishes.ToString();

        dishChosen = false;

        StartWork();
    }
}
