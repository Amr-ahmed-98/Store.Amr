namespace Domain.Models.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem product, int quantity, decimal price)
        {
            this.product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductInOrderItem product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}