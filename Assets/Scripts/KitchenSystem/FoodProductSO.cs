using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodProduct", menuName = "Kitchen/Product")]
public class FoodProductSO : ScriptableObject
{
    public string productName;
    public Sprite icon;
    public float preparationTime = 10f; // saniye cinsinden
}

