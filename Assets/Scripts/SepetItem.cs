public class SepetItem
{
    public Product product;
    public int quantity;

    public SepetItem(Product product)
    {
        this.product = product;
        this.quantity = 1;
    }

    public float GetTotalPrice()
    {
        return product.price * quantity;
    }
}

