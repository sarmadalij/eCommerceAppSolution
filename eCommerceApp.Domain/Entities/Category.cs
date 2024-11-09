using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }

        //for relationship
        public ICollection<Product>? Products { get; set; }
    }
}
