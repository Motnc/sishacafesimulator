using UnityEngine;

public class ProductButtonHandler : MonoBehaviour
{
    public ProductSO product;
    public SepetManager sepetManager;

    public void Add()
    {
        Debug.Log("Butona bas�ld�: " + product.productName);
        sepetManager.AddToCart(product);
    }

    public void Remove()
    {
        sepetManager.RemoveFromCart(product);
    }
}







