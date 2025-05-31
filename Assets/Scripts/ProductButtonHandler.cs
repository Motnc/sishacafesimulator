using UnityEngine;

public class ProductButtonHandler : MonoBehaviour
{
    public Product product;
    public SepetManager sepetManager;

    public void Add()
    {
        sepetManager.AddToCart(product);
    }

    public void Remove()
    {
        sepetManager.RemoveFromCart(product);
    }
}
