using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Model
{
    [Table("CategoryTable")]
    public class CategoryInDb
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }

    [Table("ProductTable")]
    public class ProductInDb
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("CategoryId")]
        public CategoryInDb Category { get; set; }
        public int Price { get; set; }
        public byte[] Image { get; set; }
    }

}
