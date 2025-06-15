public class SepetItem
{
    public ProductSO product;
    public int quantity;

    public SepetItem(ProductSO product)
    {
        this.product = product;
        quantity = 1;
    }

    public float GetTotalPrice()
    {
        return product.price * quantity;
    }
}






