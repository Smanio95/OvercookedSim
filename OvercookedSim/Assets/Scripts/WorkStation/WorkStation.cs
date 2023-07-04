using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkStation : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] string nowCookingStr ="Now Cooking...";
    
    public void UpdateText(bool isCooking) => text.text = isCooking ? nowCookingStr : "";

}
