using SQLite;

namespace Kursach_CRUD.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
