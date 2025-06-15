using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Product")]
public class ProductSO : ScriptableObject
{
    public string productName;
    public float price;
}
