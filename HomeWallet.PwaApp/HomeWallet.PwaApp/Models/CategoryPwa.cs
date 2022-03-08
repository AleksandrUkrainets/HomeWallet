using System.ComponentModel.DataAnnotations;

namespace HomeWallet.PwaApp.Models
{
    public class CategoryPwa
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryType { get; set; }
    }
}