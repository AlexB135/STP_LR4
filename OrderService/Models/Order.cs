namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; } = string.Empty; // Инициализация
        public decimal Price { get; set; }
    }
}
